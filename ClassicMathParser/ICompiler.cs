using System;

namespace ClassicMathParser
{
    public interface ICompiler
    {
        Func<double, double> Compile(string function);
    }
}