using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassicMathParser
{
    public class Lexer
    {
        internal readonly Token[] _tokens;
        private bool _goReverse = false;
        private Token _previousToken = null;

        private int _current = 0;

        private static Token[] Tokenize(string equation)
        {
            var RE = new Regex(@"([\+\-\*\(\)\^\/])");
            var tokens = (RE.Split(equation).Where(f => f != "").Select(f =>
                                                  {
                                                      if (f == "+")
                                                          return new Token { TokenType = TokenType.Plus };
                                                      if (f == "*")
                                                          return new Token { TokenType = TokenType.Multiply };
                                                      if (f == "-")
                                                          return new Token { TokenType = TokenType.Minus };
                                                      if (f == "/")
                                                          return new Token { TokenType = TokenType.Divide };
                                                      if (f == "x")
                                                          return new Token { TokenType = TokenType.Parameter };
                                                      if (f == "^")
                                                          return new Token { TokenType = TokenType.Quadrat };
                                                      if (f == "(")
                                                          return new Token { TokenType = TokenType.LParen };
                                                      if (f == ")")
                                                          return new Token { TokenType = TokenType.RParen };
                                                      if (f == "sin(x)")
                                                          return new Token { TokenType = TokenType.Sin };

                                                      return new Token { TokenType = TokenType.Number, Value = f.ToDouble() };
                                                  })).ToList();
            tokens.Add(new Token { TokenType = TokenType.End });
            return tokens.ToArray();
        }

        public Lexer(string input)
        {
            _tokens = Tokenize(input);
        }

        public void Reverse()
        {
            _current--;
        }

        public Token Current { get { return _tokens[_current]; } }

        public Token MoveNext()
        {
            return _tokens[_current++];
        }
    }
}
