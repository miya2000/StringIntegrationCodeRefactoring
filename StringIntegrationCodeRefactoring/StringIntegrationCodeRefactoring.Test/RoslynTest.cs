using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoslynTest
{
    [TestClass]
    public class RoslynTest
    {
        [TestMethod]
        public void StringInterpolationText_TextToken_ValueText_ExpectedUnescapedValueButRemainingEscapedCurlyBraces()
        {
            var test = @"
class A
{
    void Main()
    {
        var str = $""\""{{}}\"""";
    }
}";

            var root = SyntaxFactory.ParseCompilationUnit(test);
            var interpolatedString = root.FindNode(new TextSpan(test.IndexOf("$"), 0)) as InterpolatedStringExpressionSyntax;
            var interpolatedStringText = interpolatedString.Contents[0] as InterpolatedStringTextSyntax;
            var token = interpolatedStringText.TextToken;

            Assert.AreEqual("\\\"{{}}\\\"", token.Text);
            Assert.AreEqual("\"{{}}\"", token.Value);
            Assert.AreEqual("\"{{}}\"", token.ValueText);

            // expected.
            //Assert.AreEqual("\"{}\"", token.Value);
            //Assert.AreEqual("\"{}\"", token.ValueText);
        }

        [TestMethod]
        public void ParseString()
        {
            var token = SyntaxFactory.ParseToken("\"\\r\\n\\u0041\\x42\"");
            token.Kind().Is(SyntaxKind.StringLiteralToken);
            token.Text.Is("\"\\r\\n\\u0041\\x42\"");
            token.Value.Is("\r\nAB");

            SyntaxFactory.ParseToken("\"\\r\\n\\u0041\\x42\"").ValueText.Is("\r\nAB");
        }
    }
}