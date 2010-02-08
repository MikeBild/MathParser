using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ClassicMathParser;

namespace MathParserTests
{
    [TestFixture]
    class LexerTests
    {
        private Lexer _lexer = null;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void BasicTest()
        {
            _lexer = new Lexer("1.99+1.2");
            Assert.That(_lexer.Current.Value, Is.EqualTo(1.99));
            _lexer.MoveNext();
            Assert.That(_lexer.Current.TokenType, Is.EqualTo(TokenType.Plus));
            _lexer.MoveNext();
            Assert.That(_lexer.Current.Value, Is.EqualTo(1.2));
        }

        [Test]
        public void ParameterTest()
        {
            _lexer = new Lexer("x+1.99+1.2");            
            Assert.That(_lexer.Current.TokenType, Is.EqualTo(TokenType.Parameter));
            _lexer.MoveNext();
            Assert.That(_lexer.Current.TokenType, Is.EqualTo(TokenType.Plus));
            _lexer.MoveNext();
            Assert.That(_lexer.Current.Value, Is.EqualTo(1.99));
            _lexer.MoveNext();
            Assert.That(_lexer.Current.TokenType, Is.EqualTo(TokenType.Plus));
            _lexer.MoveNext();
            Assert.That(_lexer.Current.Value, Is.EqualTo(1.2));
        }

    }
}
