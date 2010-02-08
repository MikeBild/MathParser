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
            var actual = _sut.Parse(0, "1+2*4");
            Assert.That(actual, Is.EqualTo(1 + 2 * 4));
            actual = _sut.Parse(0, "1.1+2.3*4.8");
            Assert.That(actual, Is.EqualTo(1.1 + 2.3 * 4.8));
            actual = _sut.Parse(0, "1.1/2.3*4.8");
            Assert.That(actual, Is.EqualTo(1.1 / 2.3 * 4.8));
            actual = _sut.Parse(0, "1.1/2.3*(4.8+2)+2.1");
            Assert.That(actual, Is.EqualTo(1.1 / 2.3 * (4.8 + 2) + 2.1));
        }

        [Test]
        public void ParameterTest()
        {
            var actual = _sut.Parse(10, "x*1.1/2.3*(4.8+2)+2.1");
            Assert.That(actual, Is.EqualTo(10 * 1.1 / 2.3 * (4.8 + 2) + 2.1));
            actual = _sut.Parse(10, "1.1/2.3*(4.8+2*x)+2.1");
            Assert.That(actual, Is.EqualTo(1.1 / 2.3 * (4.8 + 2 * 10) + 2.1));
        }

        [Test]
        public void SinTest()
        {
            var actual = _sut.Parse(10, "x*sin(x)");
            Assert.That(actual, Is.EqualTo(10 * Math.Sin(10)));
            actual = _sut.Parse(10, "x*sin(x)+1");
            Assert.That(actual, Is.EqualTo(10 * Math.Sin(10) + 1));
            actual = _sut.Parse(10, "sin(x)+1*x");
            Assert.That(actual, Is.EqualTo(Math.Sin(10) + 1 * 10));
            actual = _sut.Parse(10, "(sin(x)+3)*9+1*x");
            Assert.That(actual, Is.EqualTo((Math.Sin(10) + 3) * 9 + 1 * 10));
        }
    }
}
