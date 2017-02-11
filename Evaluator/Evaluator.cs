
namespace Evaluator
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    public class Evaluate
    {

        public string eval(string expression)
        {
            if (!IsValid(expression))
            {
                return "ERROR";
            }
            try
            {
                var tree = ExpressionTreeCreator.BuildTree(expression);
                var data = tree.Evaluate();
                if (data < double.PositiveInfinity && data > double.NegativeInfinity)
                    return data.ToString();
                return "ERROR";
            }
            catch (Exception)
            {

                return "ERROR";
            }
        }

        private bool IsValid(string expression)
        {
            if (expression.Contains("++")
                || expression.Contains("(+") || expression.Contains("+)")
                || expression.Contains("-)"))
                return false;
            if (expression.Count(x => x == ')') != expression.Count(x => x == '('))
                return false;
            return true;
        }
    }
}
