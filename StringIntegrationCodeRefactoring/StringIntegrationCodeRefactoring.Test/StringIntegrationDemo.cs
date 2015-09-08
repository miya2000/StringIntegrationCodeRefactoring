using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject3
{
    [TestClass]
    public class StringIntegrationDemo
    {
        [TestMethod]
        public void StringIntegration01()
        {
            var to__ = "abcd";
            var from = "a" + "b" + "c" + "d";

            from.Is(to__);
        }

        [TestMethod]
        public void StringIntegration02()
        {
            var to__ = @"C:\path\to\dir\file.txt";
            var from = "C:\\path\\to" + @"\dir\file.txt";

            from.Is(to__);
        }

        [TestMethod]
        public void StringInterpolation01()
        {
            var a = 100;

            var to__ = $"a is [{a}]";
            var from = "a is [" + a + "]";

            from.Is(to__);
        }

        [TestMethod]
        public void StringInterpolation02()
        {
            int a = 100, b = 200;

            var to__ = $@"a + b is ""{a + b}""";
            var from = @"a + b is """ + (a + b) + @"""";

            from.Is(to__);
        }
    }
}