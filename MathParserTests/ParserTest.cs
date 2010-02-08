using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ClassicMathParser;

namespace MathParserTests
{
    [TestFixture]
    public class ParserTest
    {
        private Parser _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Parser();
        }

        [Test]
        public void BasicTest()
        {
            var actual = _sut.Parse("1+2*4");
            Assert.That(actual, Is.EqualTo(1 + 2 * 4));
            actual = _sut.Parse("1.1+2.3*4.8");
            Assert.That(actual, Is.EqualTo(1.1 + 2.3 * 4.8));
            actual = _sut.Parse("1.1/2.3*4.8");
            Assert.That(actual, Is.EqualTo(1.1 / 2.3 * 4.8));
            actual = _sut.Parse("1.1/2.3*(4.8+2)");
            Assert.That(actual, Is.EqualTo(1.1 / 2.3 * (4.8 + 2)));
        }
    }
}
