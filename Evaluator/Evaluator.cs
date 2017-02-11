
namespace Evaluator
{
    using System;
    using System.Diagnostics;

    public class Evaluate
    {

        public string eval(string expression)
        {
            if (!IsValid(expression))
            {
                return "ERROR";
            }
            var tree = ExpressionTreeCreator.BuildTree(expression);
            var data = tree.Evaluate();
            if (data < double.PositiveInfinity && data > double.NegativeInfinity)
                return data.ToString();
            return "ERROR";
        }

        private bool IsValid(string expression)
        {
            return true;
        }
    }
}
