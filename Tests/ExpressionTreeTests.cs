using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    using Evaluator;
    using NUnit.Framework;

    [TestFixture]
    public class AccountTest
    {
        private Evaluate ev = new Evaluate();

        [Test]
        public void NumbersOnly()
        {
            for (int i = 0; i < 1000; ++i)
            {
                var str = i.ToString();
                Assert.AreEqual(str, ev.eval(str));
            }
        }

        [Test]
        public void PositiveIntegersSum()
        {
            for (int i = 0; i < 100; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    Assert.AreEqual((i + j + i + j).ToString(), ev.eval($"{i}+{j}+{i}+{j}"));
                }
            }
        }
        [Test]
        public void IntegerSum()
        {
            for (int i = -100; i < 100; ++i)
            {
                for (int j = -5; j < 5; ++j)
                {
                    Assert.AreEqual((i + j + i + j).ToString(), ev.eval($"{i}+{j}+{i}+{j}"));
                }
            }
        }

        [Test]
        public void Eval_Test1()
        {
            Assert.AreEqual("18", ev.eval("2*3&2"));
        }

        [Test]
        public void Eval_Test2()
        {
            Assert.AreEqual("-240", ev.eval("(-(2 + 3)* (1 + 2)) * 4 & 2"));
        }

        [Test]
        public void Eval_Test3()
        {
            Assert.AreEqual("ERROR", (ev.eval("sqrt(-2)*2") + "     ").Substring(0, 5).ToUpper());
        }

        [Test]
        public void Eval_Test4()
        {
            Assert.AreEqual("ERROR", (ev.eval("2*5/0") + "     ").Substring(0, 5).ToUpper());
        }

        [Test]
        public void Eval_Test5()
        {
            Assert.AreEqual("-3906251", ev.eval("-5&3&2*2-1"));
        }

        [Test]
        public void Eval_Test6()
        {
            Assert.AreEqual("169", ev.eval("abs(-(-1+(2*(4--3)))&2)"));
        }
    }
}
