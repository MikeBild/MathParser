using System;
using System.Linq.Expressions;

namespace MathParser
{
    public class Compiler
    {
        public Func<double, double> Compile(string function)
        {
            var parser = new MathParser();
            Expression expression = parser.Parse(function);
            var paras = new ParameterExpression[] { Expression.Parameter(typeof(double), "x") };
            return Expression.Lambda<Func<double, double>>(expression, paras).Compile();
        }
    }
}