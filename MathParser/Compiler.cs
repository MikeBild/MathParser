using System;
using System.Linq.Expressions;

namespace MathParser
{
    public class Compiler
    {
        public Func<double> Compile(string function)
        {
            var parser = new MathParser();
            Expression expression = parser.Parse(function);
            return Expression.Lambda<Func<double>>(expression).Compile();
        }
    }
}