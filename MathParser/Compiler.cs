using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MathParser
{
    public class Compiler
    {
        public Func<double> Compile(string function)
        {
            MathParser parser = new MathParser();
            Expression expression = parser.Parse(function);
            return Expression.Lambda<Func<double>>(expression).Compile();
        }
    }
}
