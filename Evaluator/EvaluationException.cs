﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator
{
    public class EvaluationException : Exception
    {
        public EvaluationException(string msg) : base(msg)
        {

        }
    }
}
