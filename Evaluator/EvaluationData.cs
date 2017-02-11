using System.Collections.Generic;

namespace Evaluator
{
    using System;

    public struct OperatorData
    {
        public OperatorData(int priority, Associativity associativity)
        {
            Priority = priority;
            Associativity = associativity;
        }
        public int Priority { get; set; }
        public Associativity Associativity { get; set; }
    }
    static class EvaluationData
    {
        public static readonly Dictionary<string, Func<double, double, double>> OperatorsDictionary = new Dictionary
            <string, Func<double, double, double>>
            {
                {"+", (d0, d1) => d0 + d1},
                {"-", (d0, d1) => d0 - d1},
                {"*", (d0, d1) => d0*d1},
                {"/", (d0, d1) => d0/d1},
                {"&", Math.Pow},
                {"~",(d0,d1)=>-d0 }

            };

        public static readonly Dictionary<string, Func<double, double>> FunctionsDictionary = new Dictionary
            <string, Func<double, double>>
            {
                {"log",Math.Log},
                {"ln",Math.Log10},
                {"exp",Math.Exp},
                {"sqrt", Math.Sqrt},
                {"abs",  Math.Abs},
                {"atan", Math.Atan},
                {"acos",  Math.Acos} ,
                {"asin", Math.Asin},
                {"sinh",  Math.Sinh},
                {"cosh",  Math.Cosh},
                {"tanh",  Math.Tanh},
                {"tan", Math.Tan},
                {"sin", Math.Sin},
                {"cos", Math.Cos}

            };

        public static readonly Dictionary<string, OperatorData> OperatorData = new Dictionary<string, OperatorData>
        {
            { "~", new OperatorData(0,Associativity.Left)},
            { "+", new OperatorData(1,Associativity.Left)},
            { "-", new OperatorData(1,Associativity.Left)},
            { "*", new OperatorData(2,Associativity.Left)},
            { "/", new OperatorData(2,Associativity.Left)},
            { "&", new OperatorData(3,Associativity.Right)},
            { ")", new OperatorData(1,Associativity.Left)},
            { "(", new OperatorData(0,Associativity.Right)},
        };
    }
}
