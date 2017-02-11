using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    using Evaluator;
    using NUnit.Framework;
    using Evaluator = Evaluator.Evaluate;
    [TestFixture]
    public class AccountTest
    {

        [Test]
        public void NumbersOnly()
        {
            for (double i = 0; i < 1000; ++i)
            {
                var str = i.ToString();
                Assert.AreEqual(i, Evaluator.EvaluateExpression(str));
            }
        }

        [Test]
        public void PositiveIntegersSum()
        {
            for (double i = 0; i < 100; ++i)
            {
                for (double j = 0; j < 5; ++j)
                {
                    Assert.AreEqual((i + j + i + j), Evaluator.EvaluateExpression($"{i}+{j}+{i}+{j}"));
                }
            }
        }
        [Test]
        public void IntegerSum()
        {
            for (double i = -100; i < 100; ++i)
            {
                for (double j = -5; j < 5; ++j)
                {
                    Assert.AreEqual((i + j + i + j), Evaluator.EvaluateExpression($"{i}+{j}+{i}+{j}"));
                }
            }
        }

        [Test]
        public void Eval_Test1()
        {
            Assert.AreEqual(18, Evaluator.EvaluateExpression("2*3&2"));
        }
        [Test]
        public void Eval_Test7()
        {
            Assert.AreEqual(-0.826939069164175, Evaluator.EvaluateExpression("sin(cos(--17--2*1e+2))"),
                Math.Pow(1, -11));
        }

        [Test]
        public void Eval_Test2()
        {
            Assert.AreEqual(-240, Evaluator.EvaluateExpression("(-(2 + 3)* (1 + 2)) * 4 & 2"));
        }

        [Test]
        public void Eval_Test3()
        { 
            Assert.IsTrue(double.IsNaN(Evaluator.EvaluateExpression("sqrt(-2)*2")));
        }

        [Test]
        public void Eval_Test4()
        {
            Assert.IsTrue(double.IsInfinity(Evaluator.EvaluateExpression("2*5/0")));
        }

        [Test]
        public void Eval_Test5()
        {
            Assert.AreEqual(-3906251, Evaluator.EvaluateExpression("-5&3&2*2-1"));
        }

        [Test]
        public void Eval_Test6()
        {
            Assert.AreEqual(169, Evaluator.EvaluateExpression("abs(-(-1+(2*(4--3)))&2)"));
        }
        [Test]
        public void Eval_Test8()
        {
            Assert.IsTrue(double.IsNaN(Evaluator.EvaluateExpression("sqrt(-3&(1+1--1+-1))")));
        }
        [Test]
        public void Eval_Test9()
        {
            Assert.AreEqual(2.73978364189468, Evaluator.EvaluateExpression("abs(1+(2-5)--8)* sin(3 + -8) / 2.1"),
                Math.Pow(1, -11));
        }
        [Test]
        public void Eval_Test10()
        {
            Assert.AreEqual(2395.58653659743, 
                Evaluator.EvaluateExpression("(Abs(1+(2-16)-3)* sin(3 + 16) / 2.1) -12.3453* 0.45+2.4e3"),
                Math.Pow(1,-11));
        }
        [Test] 
        public void Eval_Test11()
        {
            Assert.That(() => Evaluator.EvaluateExpression("si   n (2)"), Throws.InstanceOf(typeof(EvaluationException)));
        }
        [Test] 
        public void Eval_Test12()
        {
            Assert.That(() => Evaluator.EvaluateExpression("si   n 2)"), Throws.InstanceOf(typeof(EvaluationException)));
        }
    }
}
