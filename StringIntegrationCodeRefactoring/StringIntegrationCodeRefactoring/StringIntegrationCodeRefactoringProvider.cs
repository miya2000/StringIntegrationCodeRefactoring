using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StringIntegrationCodeRefactoring
{
    [ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(StringIntegrationCodeRefactoringProvider)), Shared]
    public class StringIntegrationCodeRefactoringProvider : CodeRefactoringProvider
    {
        public const string ActionKeyStringIntegration = nameof(StringIntegrationCodeRefactoringProvider) + ".StringIntegration";

        public sealed override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var node = root.FindNode(context.Span);

            if (node == null) return;

            //process string literal or interpolated string.
            if (node.Kind() != SyntaxKind.StringLiteralExpression)
            {
                node = node.AncestorsAndSelf().OfType<InterpolatedStringExpressionSyntax>().FirstOrDefault();
            }

            if (node == null) return;

            if (node.Kind() == SyntaxKind.StringLiteralExpression || node.Kind() == SyntaxKind.InterpolatedStringExpression)
            {
                var tree = root.SyntaxTree;
                var lines = tree.GetText().Lines;

                // find add-expression in the same line (left end and right start).
                var addExpression = node.Ancestors()
                    .TakeWhile(n => n.Kind() == SyntaxKind.AddExpression)
                    .Cast<BinaryExpressionSyntax>()
                    .TakeWhile(n => lines.GetLinePosition(n.Left.Span.End).Line == lines.GetLinePosition(n.Right.Span.Start).Line)
                    .LastOrDefault();

                if (addExpression != null)
                {
                    var action = CodeAction.Create("Integrate strings", c => IntegrateStringsAsync(context.Document, addExpression, c), ActionKeyStringIntegration);
                    context.RegisterRefactoring(action);
                }
            }
        }

        private async Task<Document> IntegrateStringsAsync(Document document, BinaryExpressionSyntax addExpression, CancellationToken cancellationToken)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);

            var newExpression = ToIntegratedString(addExpression.Left, addExpression.Right);

            var newRoot = root.ReplaceNode(addExpression, newExpression);

            var newDocument = document.WithSyntaxRoot(newRoot);

            return newDocument;
        }

        private ExpressionSyntax ToIntegratedString(ExpressionSyntax left, ExpressionSyntax right)
        {
            if (left.Kind() == SyntaxKind.AddExpression)
            {
                var add = left as BinaryExpressionSyntax;
                left = ToIntegratedString(add.Left, add.Right);
            }
            if (right.Kind() == SyntaxKind.AddExpression)
            {
                var add = right as BinaryExpressionSyntax;
                right = ToIntegratedString(add.Left, add.Right);
            }

            if (left.Kind() == SyntaxKind.StringLiteralExpression && right.Kind() == SyntaxKind.StringLiteralExpression)
            {
                var leftToken = ((LiteralExpressionSyntax)left).Token;
                var rightToken = ((LiteralExpressionSyntax)right).Token;

                var value = leftToken.ValueText + rightToken.ValueText;

                var verbatimString = leftToken.Text.StartsWith("@") || rightToken.Text.StartsWith("@");
                if (verbatimString)
                {
                    var text = "@\"" + value.Replace("\"", "\"\"") + "\"";
                    return SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(text, value));
                }
                else
                {
                    return SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(value));
                }
            }
            else
            {
                var leftInterpolatedString = ToInterpolatedString(left);
                var rightInterpolatedString = ToInterpolatedString(right);

                var leftVerbatimString = leftInterpolatedString.StringStartToken.Text.Contains("@");
                var rightVerbatimString = rightInterpolatedString.StringStartToken.Text.Contains("@");

                if (leftVerbatimString != rightVerbatimString)
                {
                    if (leftVerbatimString) { rightInterpolatedString = ToVerbatimInterpolatedString(rightInterpolatedString); }
                    if (rightVerbatimString) { leftInterpolatedString = ToVerbatimInterpolatedString(leftInterpolatedString); }
                }

                return leftInterpolatedString.AddContents(rightInterpolatedString.Contents.ToArray());
            }
        }

        private InterpolatedStringExpressionSyntax ToInterpolatedString(ExpressionSyntax expression)
        {
            if (expression is InterpolatedStringExpressionSyntax)
            {
                // "${a}a" ; -> $"{a}a";
                return ((InterpolatedStringExpressionSyntax)expression).WithoutLeadingTrivia().WithoutTrailingTrivia();
            }

            // SyntaxFactory.Token(SyntaxKind.InterpolatedStringStartToken) contains WhitespaceTrivia annotated with SyntaxAnnotation.ElasticAnnotation.
            var startToken = SyntaxFactory.Token(SyntaxTriviaList.Empty, SyntaxKind.InterpolatedStringStartToken, "$\"", "$\"", SyntaxTriviaList.Empty);

            if (expression.Kind() == SyntaxKind.StringLiteralExpression)
            {
                var token = ((LiteralExpressionSyntax)expression).Token;

                var verbatimString = token.Text.StartsWith("@");
                if (verbatimString)
                {
                    startToken = SyntaxFactory.Token(SyntaxTriviaList.Empty, SyntaxKind.InterpolatedStringStartToken, "$@\"", "$@\"", SyntaxTriviaList.Empty);
                }

                var text = token.Text.Substring(verbatimString ? 2 : 1, token.Text.Length - (verbatimString ? 3: 2)).Replace("{", "{{").Replace("}", "}}"); // remove quotes and escape {}.
                var textToken = SyntaxFactory.Token(SyntaxTriviaList.Empty, SyntaxKind.InterpolatedStringTextToken, text, token.ValueText, SyntaxTriviaList.Empty);
                var content = SyntaxFactory.InterpolatedStringText(textToken);
                return SyntaxFactory.InterpolatedStringExpression(startToken).AddContents(content);
            }
            else
            {
                // (1 + 2) -> 1 + 2;
                if (expression is ParenthesizedExpressionSyntax)
                {
                    var unwraped = ((ParenthesizedExpressionSyntax)expression).Expression;
                    // ConditionalExpressionSyntax contains ":" conflicts starting a format specification.
                    if (!(unwraped is ConditionalExpressionSyntax))
                    {
                        expression = unwraped;
                    }
                }

                // "${a }" -> $"{a}"
                if (expression.HasLeadingTrivia || expression.HasTrailingTrivia)
                {
                    expression = expression.WithoutLeadingTrivia().WithoutTrailingTrivia();
                }

                var content = SyntaxFactory.Interpolation(expression);

                return SyntaxFactory.InterpolatedStringExpression(startToken).AddContents(content);
            }
        }

        private InterpolatedStringExpressionSyntax ToVerbatimInterpolatedString(InterpolatedStringExpressionSyntax original)
        {
            var startToken = SyntaxFactory.Token(SyntaxTriviaList.Empty, SyntaxKind.InterpolatedStringStartToken, "$@\"", "$@\"", SyntaxTriviaList.Empty);

            var contents = original.Contents.ToArray();

            for (int i = 0; i < contents.Length; i++)
            {
                var textContent = contents[i] as InterpolatedStringTextSyntax;

                if (textContent == null) continue;

                var textToken = textContent.TextToken;

                // InterpolatedStringTextSyntax.TextToken.ValueText is not unescape "{{" and "}}".
                //var newText = textToken.ValueText.Replace("\"", "\"\"").Replace("{", "{{").Replace("}", "}}");

                // "\\r\\n" -> "\r\n"
                var unescapeToken = SyntaxFactory.ParseToken($"\"{textToken.Text}\"");
                var newText = unescapeToken.ValueText.Replace("\"", "\"\"");

                var newTextToken = SyntaxFactory.Token(SyntaxTriviaList.Empty, SyntaxKind.InterpolatedStringTextToken, newText, textToken.ValueText, SyntaxTriviaList.Empty);
                var newContent = SyntaxFactory.InterpolatedStringText(newTextToken);

                contents[i] = newContent;
            }

            return SyntaxFactory.InterpolatedStringExpression(startToken).AddContents(contents);
        }
    }
}
