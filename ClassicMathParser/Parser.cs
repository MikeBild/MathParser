using System;
using System.Collections.Generic;

namespace ClassicMathParser
{
    /// <summary>
    ///  number     = {"0"|"1"|"2"|"3"|"4"|"5"|"6"|"7"|"8"|"9"}
    ///  factor     = number | "(" expression ")"
    ///  component  = factor [{("*" | "/") factor}]
    ///  expression = component [{("+" | "-") component}]
    /// </summary>
    public class Parser : ICompiler
    {
        private Lexer _lexer;
        private double _x;

        public Func<double, double> Compile(string function)
        {
            return input => Parse(input, function);
        }

        internal double Parse(double x, string function)
        {
            _lexer = new Lexer(function);
            _x = x;
            return Expression();
        }

        private double Expression()
        {
            double component1 = Factor();
            Token token = _lexer.MoveNext();
            while (token.TokenType == TokenType.Plus || token.TokenType == TokenType.Minus)
            {
                double component2 = Factor();
                if (token.TokenType == TokenType.Plus)
                    component1 += component2;
                else
                    component1 -= component2;
                token = _lexer.MoveNext();
            }
            _lexer.Reverse();
            return component1;
        }

        private double Factor()
        {
            double number1 = Number();
            Token token = _lexer.MoveNext();
            while (token.TokenType == TokenType.Multiply || token.TokenType == TokenType.Divide)
            {
                double number2 = Number();
                if (token.TokenType == TokenType.Multiply)
                    number1 *= number2;
                else
                    number1 /= number2;
                token = _lexer.MoveNext();
            }
            _lexer.Reverse();
            return number1;
        }

        private double Number()
        {
            double result = 0;
            Token token = _lexer.MoveNext();
           
            switch (token.TokenType)
            {
                case TokenType.LParen:
                    result = Expression();
                    token = _lexer.MoveNext();
                    if (token.TokenType != TokenType.RParen)
                        throw new InvalidOperationException(") expected!");
                    break;
                case TokenType.Number:
                    result = token.Value;
                    break;
                case TokenType.Parameter:
                    result = _x;
                    break;
                case TokenType.Sin:
                    result = Math.Sin(_x);
                    break;
                case TokenType.Quadrat:
                    result = token.Value*token.Value;
                    break;
                default:
                    throw new InvalidOperationException("Not a number");
            }

            return result;
        }

    }
}