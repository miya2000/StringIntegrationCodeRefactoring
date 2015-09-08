using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper;

namespace StringIntegrationCodeRefactoring.Test
{
    [TestClass]
    public class StringIntegrationCodeRefactoringTest
    {
        CodeRefactoringVerifier Verifier { get; } = new CodeRefactoringVerifier(new StringIntegrationCodeRefactoringProvider());

        #region IntegrateString_01_String_String
        [TestMethod]
        public void IntegrateString_01_String_String()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = ""aa\r\n\""aa\""\r\n"" + ""\""bb\""\r\nbb"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = ""aa\r\n\""aa\""\r\n\""bb\""\r\nbb"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region IntegrateString_01_String_VerbatimString
        [TestMethod]
        public void IntegrateString_01_String_VerbatimString()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = ""aa\r\n\""aa\""\r\n"" + @""""""bb""""
bb"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
""""bb""""
bb"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region IntegrateString_01_VerbatimString_String
        [TestMethod]
        public void IntegrateString_01_VerbatimString_String()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
"" + ""\""bb\""\r\nbb"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
""""bb""""
bb"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region IntegrateString_01_VerbatimString_VerbatimString
        [TestMethod]
        public void IntegrateString_01_VerbatimString_VerbatimString()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
"" + @""""""bb""""
bb"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
""""bb""""
bb"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region IntegrateString_02_String_String_String
        [TestMethod]
        public void IntegrateString_02_String_String_String()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = ""aa\r\n\""aa\""\r\n"" + ""\""bb\""\r\n"" + ""\""cc\""\r\ncc"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = ""aa\r\n\""aa\""\r\n\""bb\""\r\n\""cc\""\r\ncc"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region IntegrateString_02_String_String_VerbatimString
        [TestMethod]
        public void IntegrateString_02_String_String_VerbatimString()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = ""aa\r\n\""aa\""\r\n"" + ""\""bb\""\r\n"" + @""""""cc""""
cc"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
""""bb""""
""""cc""""
cc"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region IntegrateString_02_String_VerbatimString_String
        [TestMethod]
        public void IntegrateString_02_String_VerbatimString_String()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = ""aa\r\n\""aa\""\r\n"" + @""""""bb""""
"" + ""\""cc\""\r\ncc"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
""""bb""""
""""cc""""
cc"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region IntegrateString_02_String_VerbatimString_VerbatimString
        [TestMethod]
        public void IntegrateString_02_String_VerbatimString_VerbatimString()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = ""aa\r\n\""aa\""\r\n"" + @""""""bb""""
"" + @""""""cc""""
cc"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
""""bb""""
""""cc""""
cc"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region IntegrateString_02_VerbatimString_String_String
        [TestMethod]
        public void IntegrateString_02_VerbatimString_String_String()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
"" + ""\""bb\""\r\n"" + ""\""cc\""\r\ncc"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
""""bb""""
""""cc""""
cc"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region IntegrateString_02_VerbatimString_String_VerbatimString
        [TestMethod]
        public void IntegrateString_02_VerbatimString_String_VerbatimString()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
"" + ""\""bb\""\r\n"" + @""""""cc""""
cc"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
""""bb""""
""""cc""""
cc"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region IntegrateString_02_VerbatimString_VerbatimString_String
        [TestMethod]
        public void IntegrateString_02_VerbatimString_VerbatimString_String()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
"" + @""""""bb""""
"" + ""\""cc\""\r\ncc"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
""""bb""""
""""cc""""
cc"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region IntegrateString_02_VerbatimString_VerbatimString_VerbatimString
        [TestMethod]
        public void IntegrateString_02_VerbatimString_VerbatimString_VerbatimString()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
"" + @""""""bb""""
"" + @""""""cc""""
cc"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""aa""""
""""bb""""
""""cc""""
cc"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion

        #region StringInterpolation_01_String_int
        [TestMethod]
        public void StringInterpolation_01_String_int()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = ""aa{aa}"" + 100;
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = $""aa{{aa}}{100}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_01_String_variable
        [TestMethod]
        public void StringInterpolation_01_String_variable()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = ""aa{aa}"" + aa;
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""aa{{aa}}{aa}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_01_String_expression
        [TestMethod]
        public void StringInterpolation_01_String_expression()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = ""aa{aa}"" + (100 + 200);
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = $""aa{{aa}}{100 + 200}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_01_String_StringInterpolation
        [TestMethod]
        public void StringInterpolation_01_String_StringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = ""aa{aa}"" + $""{{{aa}}}"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""aa{{aa}}{{{aa}}}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_01_String_VerbatimStringInterpolation
        [TestMethod]
        public void StringInterpolation_01_String_VerbatimStringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var bb = 200;
            var str = ""aa\r\n\""aa\""\r\n"" + $@""""""{{{bb}}}""""
bb"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var bb = 200;
            var str = $@""aa
""""aa""""
""""{{{bb}}}""""
bb"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_01_int_String
        [TestMethod]
        public void StringInterpolation_01_int_String()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = 100 + ""aa{aa}"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = $""{100}aa{{aa}}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_01_variable_String
        [TestMethod]
        public void StringInterpolation_01_variable_String()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = aa + ""aa{aa}"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""{aa}aa{{aa}}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_01_expression_String
        [TestMethod]
        public void StringInterpolation_01_expression_String()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = (100 + 200) + ""aa{aa}"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = $""{100 + 200}aa{{aa}}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_01_StringInterpolation_String
        [TestMethod]
        public void StringInterpolation_01_StringInterpolation_String()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""{{{aa}}}"" + ""aa{aa}"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""{{{aa}}}aa{{aa}}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_01_VerbatimStringInterpolation_String
        [TestMethod]
        public void StringInterpolation_01_VerbatimStringInterpolation_String()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var bb = 200;
            var str = $@""""""{{{bb}}}""""
bb"" + ""aa\r\n\""aa\""\r\n"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var bb = 200;
            var str = $@""""""{{{bb}}}""""
bbaa
""""aa""""
"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_02_VerbatimString_int
        [TestMethod]
        public void StringInterpolation_02_VerbatimString_int()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""{aa}""""
"" + 100;
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = $@""aa
""""{{aa}}""""
{100}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_02_VerbatimString_variable
        [TestMethod]
        public void StringInterpolation_02_VerbatimString_variable()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = @""aa
""""{aa}""""
"" + aa;
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""aa
""""{{aa}}""""
{aa}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_02_VerbatimString_expression
        [TestMethod]
        public void StringInterpolation_02_VerbatimString_expression()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = @""aa
""""{aa}""""
"" + (100 + 200);
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = $@""aa
""""{{aa}}""""
{100 + 200}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_02_VerbatimString_StringInterpolation
        [TestMethod]
        public void StringInterpolation_02_VerbatimString_StringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var bb = 200;
            var str = @""aa
""""{aa}""""
"" + $""\""{{{bb}}}\""\r\nbb"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var bb = 200;
            var str = $@""aa
""""{{aa}}""""
""""{{{bb}}}""""
bb"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_02_VerbatimString_VerbatimStringInterpolation
        [TestMethod]
        public void StringInterpolation_02_VerbatimString_VerbatimStringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var bb = 200;
            var str = @""aa
""""{aa}""""
"" + $@""""""{{{bb}}}""""
bb"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var bb = 200;
            var str = $@""aa
""""{{aa}}""""
""""{{{bb}}}""""
bb"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_02_int_VerbatimString
        [TestMethod]
        public void StringInterpolation_02_int_VerbatimString()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = 100 + @""aa
""""{aa}""""
"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = $@""{100}aa
""""{{aa}}""""
"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_02_variable_VerbatimString
        [TestMethod]
        public void StringInterpolation_02_variable_VerbatimString()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = aa + @""aa
""""{aa}""""
"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""{aa}aa
""""{{aa}}""""
"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_02_expression_VerbatimString
        [TestMethod]
        public void StringInterpolation_02_expression_VerbatimString()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = (100 + 200) + @""aa
""""{aa}""""
"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = $@""{100 + 200}aa
""""{{aa}}""""
"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_02_StringInterpolation_VerbatimString
        [TestMethod]
        public void StringInterpolation_02_StringInterpolation_VerbatimString()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var bb = 200;
            var str = $""\""{{{bb}}}\""\r\nbb"" + @""aa
""""{aa}""""
"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var bb = 200;
            var str = $@""""""{{{bb}}}""""
bbaa
""""{{aa}}""""
"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_02_VerbatimStringInterpolation_VerbatimString
        [TestMethod]
        public void StringInterpolation_02_VerbatimStringInterpolation_VerbatimString()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var bb = 200;
            var str = $@""""""{{{bb}}}""""
bb"" + @""aa
""""{aa}""""
"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var bb = 200;
            var str = $@""""""{{{bb}}}""""
bbaa
""""{{aa}}""""
"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_03_StringInterpolation_int
        [TestMethod]
        public void StringInterpolation_03_StringInterpolation_int()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""aa{aa}"" + 100;
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""aa{aa}{100}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_03_StringInterpolation_variable
        [TestMethod]
        public void StringInterpolation_03_StringInterpolation_variable()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""aa{aa}"" + aa;
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""aa{aa}{aa}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_03_StringInterpolation_expression
        [TestMethod]
        public void StringInterpolation_03_StringInterpolation_expression()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""aa{aa}"" + (100 + 200);
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""aa{aa}{100 + 200}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_03_StringInterpolation_StringInterpolation
        [TestMethod]
        public void StringInterpolation_03_StringInterpolation_StringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""aa{aa}"" + $""{{{aa}}}"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""aa{aa}{{{aa}}}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_03_StringInterpolation_VerbatimStringInterpolation
        [TestMethod]
        public void StringInterpolation_03_StringInterpolation_VerbatimStringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var bb = 200;
            var str = $""aa{aa}"" + $@""""""{{{bb}}}""""
bb"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var bb = 200;
            var str = $@""aa{aa}""""{{{bb}}}""""
bb"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_03_int_StringInterpolation
        [TestMethod]
        public void StringInterpolation_03_int_StringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = 100 + $""aa{aa}"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""{100}aa{aa}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_03_variable_StringInterpolation
        [TestMethod]
        public void StringInterpolation_03_variable_StringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = aa + $""aa{aa}"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""{aa}aa{aa}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_03_expression_StringInterpolation
        [TestMethod]
        public void StringInterpolation_03_expression_StringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = (100 + 200) + $""aa{aa}"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $""{100 + 200}aa{aa}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_03_VerbatimStringInterpolation_StringInterpolation
        [TestMethod]
        public void StringInterpolation_03_VerbatimStringInterpolation_StringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var bb = 200;
            var str = $@""""""{{{bb}}}""""
bb"" + $""aa{aa}"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var bb = 200;
            var str = $@""""""{{{bb}}}""""
bbaa{aa}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_04_VerbatimStringInterpolation_int
        [TestMethod]
        public void StringInterpolation_04_VerbatimStringInterpolation_int()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""aa{aa}
"" + 100;
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""aa{aa}
{100}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_04_VerbatimStringInterpolation_variable
        [TestMethod]
        public void StringInterpolation_04_VerbatimStringInterpolation_variable()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""aa{aa}
"" + aa;
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""aa{aa}
{aa}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_04_VerbatimStringInterpolation_expression
        [TestMethod]
        public void StringInterpolation_04_VerbatimStringInterpolation_expression()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""aa{aa}
"" + (100 + 200);
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""aa{aa}
{100 + 200}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_04_VerbatimStringInterpolation_StringInterpolation
        [TestMethod]
        public void StringInterpolation_04_VerbatimStringInterpolation_StringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""aa{aa}
"" + $""{{{aa}}}"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""aa{aa}
{{{aa}}}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_04_VerbatimStringInterpolation_VerbatimStringInterpolation
        [TestMethod]
        public void StringInterpolation_04_VerbatimStringInterpolation_VerbatimStringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var bb = 200;
            var str = $@""aa{aa}
"" + $@""""""{{{bb}}}""""
bb"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var bb = 200;
            var str = $@""aa{aa}
""""{{{bb}}}""""
bb"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_04_int_VerbatimStringInterpolation
        [TestMethod]
        public void StringInterpolation_04_int_VerbatimStringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = 100 + $@""aa{aa}
"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""{100}aa{aa}
"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_04_variable_VerbatimStringInterpolation
        [TestMethod]
        public void StringInterpolation_04_variable_VerbatimStringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = aa + $@""aa{aa}
"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""{aa}aa{aa}
"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_04_expression_VerbatimStringInterpolation
        [TestMethod]
        public void StringInterpolation_04_expression_VerbatimStringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = (100 + 200) + $@""aa{aa}
"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var str = $@""{100 + 200}aa{aa}
"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion
        #region StringInterpolation_04_StringInterpolation_VerbatimStringInterpolation
        [TestMethod]
        public void StringInterpolation_04_StringInterpolation_VerbatimStringInterpolation()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var bb = 200;
            var str = $""{bb}bb"" + $@""aa{aa}
"";
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var aa = 100;
            var bb = 200;
            var str = $@""{bb}bbaa{aa}
"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"aa"));
            actual.Is(expected);
        }
        #endregion

        #region UnwrapParenthesizedExpression
        [TestMethod]
        public void UnwrapParenthesizedExpression()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = ""result:"" + (100 + 200);
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = $""result:{100 + 200}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"result"));
            actual.Is(expected);
        }
        #endregion
        #region UnwrapParenthesizedExpression_ExceptsConditionalExpression
        [TestMethod]
        public void UnwrapParenthesizedExpression_ExceptsConditionalExpression()
        {
            var test = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = ""result:"" + (DateTime.Today.DayOfWeek == DayOfWeek.Sunday ? 1 : 0);
        }
    }
}";

            var expected = @"
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            var str = $""result:{(DateTime.Today.DayOfWeek == DayOfWeek.Sunday ? 1 : 0)}"";
        }
    }
}";

            var actual = Verifier.GetRefactoringResult(test, test.IndexOf("\"result"));
            actual.Is(expected);
        }
        #endregion

    }
}
