using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    using System.Linq;
    using Evaluator;

    [TestClass]
    public class TokenTests
    {
        [TestMethod]
        public void Operators()
        {
            string operators = "+-/*&";
            foreach (char t in operators)
            {
                var token = new Token(t, TokenType.Operator);
                Assert.AreEqual(TokenType.Operator, token.Type);

            }
        }

        [TestMethod]
        public void TokenizeTest()
        {
            string str = "sqrt (sin(2 + 3)*cos (1+2)) * 4 & 2";
            string output = "sqrt|(|sin|(|2|+|3|)|*|cos|(|1|+|2|)|)|*|4|&|2";
            var tokens = ExpressionTreeCreator.Tokenize(str);
            Assert.AreEqual(output, tokens.Aggregate("", (x, y) => x + "|" + y.Value).Substring(1));
            str = "((2 + 3) * (1 + 2)) * 4 & 2";
            output = "(|(|2|+|3|)|*|(|1|+|2|)|)|*|4|&|2";
            tokens = ExpressionTreeCreator.Tokenize(str);
            Assert.AreEqual(output, tokens.Aggregate("", (x, y) => x + "|" + y.Value).Substring(1));
            str = " ((2 + 3) * (1 + 2)) * 4 & -2";
            output = "(|(|2|+|3|)|*|(|1|+|2|)|)|*|4|&|~|2";
            tokens = ExpressionTreeCreator.Tokenize(str);
            Assert.AreEqual(output, tokens.Aggregate("", (x, y) => x + "|" + y.Value).Substring(1));
            str = " abs(-2 * 1e-3)";
            output = "abs|(|~|2|*|1e-3|)";
            tokens = ExpressionTreeCreator.Tokenize(str);
            Assert.AreEqual(output, tokens.Aggregate("", (x, y) => x + "|" + y.Value).Substring(1));
            str = "10+-10+10+-10-10";
            output = "10|+|~|10|+|10|+|~|10|-|10";
            tokens = ExpressionTreeCreator.Tokenize(str);
            Assert.AreEqual(output, tokens.Aggregate("", (x, y) => x + "|" + y.Value).Substring(1));
        }

        [TestMethod]
        public void TokenizeNegativeTest()
        {
            var str = " - 100 + -5 + -100 + -5";
            var output = "~|100|+|~|5|+|~|100|+|~|5";
            var tokens = ExpressionTreeCreator.Tokenize(str);
            Assert.AreEqual(output, tokens.Aggregate("", (x, y) => x + "|" + y.Value).Substring(1));
        }

        [TestMethod]
        public void TokenizeNegativeBracket()
        {
            var str = "-5 * 1";
            var output = "~|5|*|1";
            var tokens = ExpressionTreeCreator.Tokenize(str);
            Assert.AreEqual(output, tokens.Aggregate("", (x, y) => x + "|" + y.Value).Substring(1));

            str = "-(5 * 1)";
            output = "neg|(|5|*|1|)";
            tokens = ExpressionTreeCreator.Tokenize(str);
            Assert.AreEqual(output, tokens.Aggregate("", (x, y) => x + "|" + y.Value).Substring(1));
        }
    }
}
