using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClassicMathParser
{
    public class Lexer
    {
        internal readonly Token[] _tokens;
        private int _current;
        private bool _goReverse;
        private Token _previousToken;

        public Lexer(string input)
        {
            _tokens = Tokenize(input.Replace(" ", ""));
        }

        public Token Current
        {
            get { return _tokens[_current]; }
        }

        public void Reverse()
        {
            _current--;
        }

        public Token MoveNext()
        {
            return _tokens[_current++];
        }

        private static Token[] Tokenize(string equation)
        {
            var RE = new Regex(@"(sin\(x\))([\+\-\*\(\)\/\^])|([\+\-\*\(\)\/\^])(sin\(x\))|([\+\-\*\(\)\/\^])");

            List<Token> tokens = (RE.Split(equation)
                .Where(f => f != "")
                .Select(f =>
                {
                    switch (f)
                    {
                        case "+":
                            return new Token
                            {
                                TokenType =
                                    TokenType
                                    .Plus
                            };
                            break;
                        case "*":
                            return new Token
                            {
                                TokenType =
                                    TokenType
                                    .Multiply
                            };
                            break;
                        case "-":
                            return new Token
                            {
                                TokenType =
                                    TokenType
                                    .Minus
                            };
                            break;
                        case "/":
                            return new Token
                            {
                                TokenType =
                                    TokenType
                                    .Divide
                            };
                            break;
                        case "x":
                            return new Token
                            {
                                TokenType =
                                    TokenType
                                    .
                                    Parameter
                            };
                            break;
                        case "^2":
                            return new Token
                            {
                                TokenType =
                                    TokenType
                                    .Quadrat
                            };
                            break;
                        case "(":
                            return new Token
                            {
                                TokenType =
                                    TokenType
                                    .LParen
                            };
                        case ")":
                            return new Token
                            {
                                TokenType =
                                    TokenType
                                    .RParen
                            };
                        case "sin(x)":
                            return new Token
                            {
                                TokenType =
                                    TokenType
                                    .Sin
                            };
                        default:
                            return new Token
                            {
                                TokenType =
                                    TokenType.
                                    Number,
                                Value =
                                    f.ToDouble()
                            };
                    }
                }))
                .ToList();
            tokens.Add(new Token { TokenType = TokenType.End });
            return tokens.ToArray();
        }
    }
}