﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MathParser;
using NUnit.Framework;

namespace MathParserTests
{
    [TestFixture]
    class ParserOperationTests
    {
        private Compiler _compiler = null;
        private Func<double> _sut = null;

        [SetUp]
        public void Setup()
        {
            _compiler = new Compiler();
        }

        [Test]
        public void ParserParsed1NumberTest()
        {
            _sut = _compiler.Compile("1.11");
            Assert.That(_sut(), Is.EqualTo(1.11));
        }

        [Test]
        public void ParserParsed1AddOperationTest()
        {
            _sut = _compiler.Compile("1.11+123.23");
            Assert.That(_sut(), Is.EqualTo(1.11 + 123.23));
        }

        [Test]
        public void ParserParsed2AddOperationTest()
        {
            _sut = _compiler.Compile("1.11+123.23+99.2");
            Assert.That(_sut(), Is.EqualTo(1.11 + 123.23 + 99.2));
        }

        [Test]
        public void ParserParsed2MultiplyOperationTest()
        {
            _sut = _compiler.Compile("1.11*123.23");
            Assert.That(_sut(), Is.EqualTo(1.11 * 123.23));
        }

        [Test]
        public void ParserParsed3MultiplyOperationTest()
        {
            _sut = _compiler.Compile("1.11*123.23*99.5");
            Assert.That(_sut(), Is.EqualTo(1.11 * 123.23 * 99.5));
        }

        [Test]
        public void ParserParsed4MultiplyOperationTest()
        {
            _sut = _compiler.Compile("1.11*123.23*99.5*22.8");
            Assert.That(_sut(), Is.EqualTo(1.11 * 123.23 * 99.5 * 22.8));
        }

        [Test]
        public void ParserParsed2MultiplyWith1AddOperationTest()
        {
            _sut = _compiler.Compile("1.11*123.23+99.5*993.3");
            Assert.That(_sut(), Is.EqualTo(1.11 * 123.23 + 99.5 * 993.3));

            _sut = _compiler.Compile("1.11*123.23*99.5+993.3");
            Assert.That(_sut(), Is.EqualTo(1.11 * 123.23 * 99.5 + 993.3));
        }

        [Test]
        public void ParserParsedMultipleOperationTest()
        {
            _sut = _compiler.Compile("1.11*123.23*99.5+993.3*22.3+44.2*33.3");
            Assert.That(_sut(), Is.EqualTo(1.11 * 123.23 * 99.5 + 993.3 * 22.3 + 44.2 * 33.3));

            _sut = _compiler.Compile("1.11+123.23+99.5+993.3+22.3+44.2+33.3");
            Assert.That(_sut(), Is.EqualTo(1.11 + 123.23 + 99.5 + 993.3 + 22.3 + 44.2 + 33.3));

            _sut = _compiler.Compile("1.11*123.23*99.5*993.3*22*44.2*33.3");
            Assert.That(_sut(), Is.EqualTo(1.11 * 123.23 * 99.5 * 993.3 * 22 * 44.2 * 33.3));

        }

    }
}
