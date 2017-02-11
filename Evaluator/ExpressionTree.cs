using System;

namespace Evaluator
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class ExpressionTreeNode
    {
        public ExpressionTreeNode Right { get; set; }
        public ExpressionTreeNode Left { get; set; }
        private Token _token { get; set; }
        private Func<double, double, double> evaluateFunc { get; set; }

        public ExpressionTreeNode(Token token)
        {
            _token = token;
            if (token.Type == TokenType.Function)
            {
                if (EvaluationData.FunctionsDictionary.ContainsKey(token.Value))
                {
                    evaluateFunc = (d1, d2) => EvaluationData.FunctionsDictionary[token.Value](d1);
                }
                else
                {
                    throw new ArgumentException($"Problem with function '{token.Value}'");
                }
            }
            else if (token.Type == TokenType.Number)
            {
                evaluateFunc = (d1, d2) => double.Parse(_token.Value, CultureInfo.InvariantCulture);
            }
            else if (token.Type == TokenType.Operator)
            {
                if (EvaluationData.OperatorsDictionary.ContainsKey(token.Value))
                {
                    evaluateFunc = EvaluationData.OperatorsDictionary[token.Value];
                }
                else if(token.Value!=")" && token.Value!="(")
                {
                    throw new ArgumentException($"Problem with operator '{token.Value}'");
                }
            }
            else
            {
                throw new ArgumentException($"Problem with tokenem {token.Type}");
            }
        }

        public double Evaluate()
        {
            if (Right != null && Left != null)
                return evaluateFunc(Left.Evaluate(), Right.Evaluate());
            if (Left != null)
                return evaluateFunc(Left.Evaluate(), 0);
            return evaluateFunc(0, 0);
        }
    }

    public class ExpressionTree
    {
        private Stack<ExpressionTreeNode> Stack = new Stack<ExpressionTreeNode>();

        public void Add(Token token)
        {

            try
            {
                if (token.Type == TokenType.Number)
                {
                    Stack.Push(new ExpressionTreeNode(token));
                }
                else if (token.Type == TokenType.Function)
                {
                    var tmp = Stack.Pop();
                    var func = new ExpressionTreeNode(token) {Left = tmp};
                    Stack.Push(func);
                }
                else if (token.Value == "~")
                {
                    var tmp = Stack.Pop();
                    var func = new ExpressionTreeNode(token) {Left = tmp};
                    Stack.Push(func);
                }
                else if (token.Type == TokenType.Operator)
                {
                    var tmp1 = Stack.Pop();
                    var tmp2 = Stack.Pop();
                    var func = new ExpressionTreeNode(token) {Left = tmp2, Right = tmp1};
                    Stack.Push(func);
                }
                else
                {
                    throw new ArgumentException("Problem with token?");
                }
            }
            catch (Exception)
            {
                
            }

        }

        public double Evaluate()
        {
            return Stack.Peek().Evaluate();
        }
    }

}
