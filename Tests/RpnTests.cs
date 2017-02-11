using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Evaluator;

    [TestClass]
    public class RpnTests
    {
        private string TokensToString(List<Token> list)
        { 
            return list.Select(x => x.Value).Aggregate("", (x, y) => x + " " + y).Substring(1);
        }
        [TestMethod]
        public void TestMethod1()
        { 
            var list = new List<Token>()
            {
                new Token("3", TokenType.Number),
                new Token("-", TokenType.Operator),
                new Token("2", TokenType.Number),
            };
            var output = ExpressionTreeCreator.ToPostfix(list);
            Assert.AreEqual("3 2 -", TokensToString(output));
        }
        [TestMethod]
        public void TestMethod2()
        {
            var list = new List<Token>()
            {
                new Token("(", TokenType.Operator),
                new Token("12", TokenType.Number),
                new Token("-", TokenType.Operator),
                new Token("3", TokenType.Number),
                new Token(")", TokenType.Operator),
                new Token("/", TokenType.Operator),
                new Token("3", TokenType.Number),
            };
            var output = ExpressionTreeCreator.ToPostfix(list);
            Assert.AreEqual("12 3 - 3 /", TokensToString(output));
        }
        [TestMethod]
        public void TestMethod3()
        {
            var list = new List<Token>()
            {
                new Token("-1", TokenType.Number),
                new Token("*", TokenType.Operator),
                new Token("3", TokenType.Number), 
            };
            var output = ExpressionTreeCreator.ToPostfix(list);
            Assert.AreEqual("-1 3 *", TokensToString(output));
        }
    }
}
