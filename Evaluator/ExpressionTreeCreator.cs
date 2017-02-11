using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{
    public static class ExpressionTreeCreator
    {
        enum MinusState
        {
            Normal, Negation, Scientific, None
        }

        public static List<Token> ToPostfix(List<Token> tokens)
        {
            Queue<Token> output = new Queue<Token>();
            Stack<Token> stack = new Stack<Token>();
            bool unaryMinus = false;
            for (int index = 0; index < tokens.Count; index++)
            {
                var token = tokens[index];
                if (token.Type == TokenType.Number)
                {
                    if (unaryMinus)
                    {
                        unaryMinus = false;
                    }
                    output.Enqueue(token);
                }
                else if (token.Value == "~")
                {
                    unaryMinus = !unaryMinus;
                    stack.Push(token);
                }
                else if (token.Type == TokenType.Function)
                {
                    stack.Push(token);
                }
                else if (token.Type == TokenType.Operator && token.Value != ")" && token.Value != "(")
                {
                    unaryMinus = false;
                    var data = EvaluationData.OperatorData[token.Value];
                    while (stack.Count > 0 && stack.Peek().Type == TokenType.Operator)
                    {
                        var tmp = stack.Peek();
                        var tmpData = EvaluationData.OperatorData[tmp.Value];
                        if ((data.Associativity == Associativity.Left && data.Priority <= tmpData.Priority)
                            ||
                            (data.Associativity == Associativity.Right && data.Priority < tmpData.Priority)
                        )
                        {
                            tmp = stack.Pop();
                            output.Enqueue(tmp);
                        }
                        else
                        {
                            break;
                        }
                    }
                    stack.Push(token);
                }
                else if (token.Value == "(")
                {
                    unaryMinus = false;
                    stack.Push(token);
                }
                else if (token.Value == ")")
                {
                    unaryMinus = false;
                    while (stack.Count > 0 && stack.Peek().Value != "(")
                    {
                        output.Enqueue(stack.Pop());
                    }
                    if (stack.Count > 0)
                    {
                        if (stack.Peek().Value != "(")
                        {
                            throw new ArgumentException("Left bracket not found");
                        }
                        stack.Pop();
                        if (stack.Count > 0 && (stack.Peek().Type == TokenType.Function || stack.Peek().Value == "~"))
                        {
                            output.Enqueue(stack.Pop());
                        }
                    }
                }
            }
            while (stack.Count > 0)
            {
                var tmp = stack.Pop();
                if (tmp.Value == ")" || tmp.Value == "(")
                    continue;
                output.Enqueue(tmp);
            }
            return output.ToList();
        }

        public static ExpressionTree BuildTree(string input)
        {
            var tokens = Tokenize(input);
            tokens = ToPostfix(tokens);
            var tree = new ExpressionTree();
            foreach (var token in tokens)
            {
                tree.Add(token);
            }
            return tree;
        }

        public static List<Token> Tokenize(string input)
        {
            var list = new List<Token>();
            Action<string> dumpNumber =
                c =>
                {
                    if (c != "")
                        list.Add(new Token(c, TokenType.Number));
                };
            Action<string> dumpFunction =
                c =>
                {
                    if (c != "")
                        list.Add(new Token(c, TokenType.Function));
                };
            string lastNumber = "";
            string lastCharacter = "";
            var minusState = MinusState.Negation;
            foreach (char c in input)
            {
                if (c == ' ')
                {
                    dumpFunction(lastCharacter);
                    lastCharacter = "";
                    dumpNumber(lastNumber);
                    lastNumber = "";
                }
                else if (char.IsLetter(c))
                {
                    if (c == 'e' && lastNumber != "")
                    {
                        lastNumber += 'e';
                        minusState = MinusState.Scientific;
                    }
                    else
                    {
                        lastCharacter += c;
                        dumpNumber(lastNumber);
                        minusState = MinusState.Normal;
                    }
                }
                else if (c == ')')
                {
                    dumpNumber(lastNumber);
                    lastNumber = "";
                    dumpFunction(lastCharacter);
                    lastCharacter = "";
                    var token = new Token(")", TokenType.Operator);
                    list.Add(token);
                    minusState = MinusState.Normal;
                }
                else if (c == '(')
                {
                    if (lastNumber != "")
                    {
                        throw new ArgumentException($"Number cannot be so close to (, {lastNumber}");
                    }
                    dumpFunction(lastCharacter);
                    lastCharacter = "";
                    if (list.Count > 0 && list.Last().Value == "~")
                    {
                        list.RemoveAt(list.Count - 1);
                        var func = new Token("neg", TokenType.Function);
                        list.Add(func);
                    }
                    minusState = MinusState.Negation;
                    var token = new Token("(", TokenType.Operator);
                    list.Add(token);
                }
                else if (IsOperator(c.ToString()))
                {
                    if (c == '-' && (minusState == MinusState.Negation))
                    {
                        dumpNumber(lastNumber);
                        lastNumber = "";
                        var token = new Token("~", TokenType.Operator);
                        list.Add(token);
                    }
                    else if (((c == '-') || c == '+') && lastNumber.Length > 0 && lastNumber[lastNumber.Length - 1] == 'e')
                    {
                        lastNumber += c;
                    }
                    else
                    {
                        dumpNumber(lastNumber);
                        lastNumber = "";
                        var token = new Token(c, TokenType.Operator);
                        list.Add(token);
                        minusState = MinusState.Negation;
                    }
                }
                else if (char.IsNumber(c) || c == '.')
                {
                    lastNumber += c;
                    minusState = MinusState.Normal;
                }
                else
                {
                    throw new ArgumentException($"Wrong input {c}");
                }
            }
            if (lastNumber != "")
                dumpNumber(lastNumber);
            if (lastCharacter != "")
                dumpFunction(lastCharacter);

            return list;
        }

        private static bool IsOperator(string c)
        {
            return EvaluationData.OperatorsDictionary.ContainsKey(c);
        }
    }
}
