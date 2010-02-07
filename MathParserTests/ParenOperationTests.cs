using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParser;
using NUnit.Framework;

namespace MathParserTests
{
    [TestFixture]
    public class ParenOperationTests
    {
        private Compiler _compiler = null;
        private Func<double, double> _sut = null;

        [SetUp]
        public void Setup()
        {
            _compiler = new Compiler();
        }

        [Test]
        public void ParserBasicParenTest()
        {
            _sut = _compiler.Compile("(1.11+1.2)");
            Assert.That(_sut(1.0), Is.EqualTo((1.11 + 1.2)));
        }
    }
}
