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
        public void Eval_Test7()
        {
            Assert.AreEqual("-0.826939069164175", ev.eval("sin(cos(--17--2*1e+2))"));
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
        [Test]
        public void Eval_Test8()
        {
            Assert.AreEqual("ERROR", ev.eval("sqrt(-3&(1+1--1+-1))"));
        }
        [Test]
        public void Eval_Test9()
        {
            Assert.AreEqual("2.73978364189468", ev.eval("abs(1+(2-5)--8)* sin(3 + -8) / 2.1"));
        }
        [Test]
        public void Eval_Test10()
        {
            Assert.AreEqual("2395.58653659743", ev.eval("(Abs(1+(2-16)-3)* sin(3 + 16) / 2.1) -12.3453* 0.45+2.4e3"));
        }
        [Test]
        public void Eval_Test11()
        {
            Assert.AreEqual("ERROR", ev.eval("si   n (2)"));
        }
    }
}
