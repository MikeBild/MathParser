using System;
using System.Collections.Generic;

namespace ClassicMathParser
{
    public class Parser
    {
        /*
        number     = {"0"|"1"|"2"|"3"|"4"|"5"|"6"|"7"|"8"|"9"}
        factor     = number | "(" expression ")"
        component  = factor [{("*" | "/") factor}]
        expression = component [{("+" | "-") component}]
        */

        private Lexer _lexer;


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
            if (token.TokenType == TokenType.LParen)
            {
                result = Expression();
                if (token.TokenType != TokenType.RParen)
                    throw new InvalidOperationException(") expected!");
            }
            else if (token.TokenType == TokenType.Number)
            {
                result = token.Value;
            }
            else
            {
                throw new InvalidOperationException("Not a number");
            }
            return result;
        }

        public double Parse(string function)
        {
            _lexer = new Lexer(function);
            return Expression();
        }
    }
}