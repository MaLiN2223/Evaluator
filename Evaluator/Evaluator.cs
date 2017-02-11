
namespace Evaluator
{
    using System;
    using System.Data;
    using System.Linq;

    public static class Evaluate
    {
        public static double EvaluateExpression(string expression)
        {
            if (!IsValid(expression))
            {
                throw new EvaluationException("Expression is not valid");
            }
            try
            {
                var tree = ExpressionTreeCreator.BuildTree(expression);
                var data = tree.Evaluate();
                return data;
            }
            catch (InvalidOperationException exc)
            {
                if (exc.Message.Contains("Stack empty"))
                {
                    throw new EvaluationException("Expression is not valid");
                }
                throw;
            }

        }

        private static bool IsValid(string expression)
        {
            if (expression.Count(x => x == ')') != expression.Count(x => x == '('))
                return false;
            if (expression.Contains("++")
                || expression.Contains("(+")
                || expression.Contains("+)")
                || expression.Contains("-)"))
                return false;
            return true;
        }
    }
}
