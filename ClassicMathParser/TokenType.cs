using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassicMathParser
{
    public enum TokenType
    {
        Plus = 0,
        Minus = 1,
        Multiply = 2,
        Divide = 3,

        Number = 4,

        LParen = 5,
        RParen = 6,

        End = 7,

        Parameter = 8,

        Quadrat = 9,

        Sin = 10,

        None = 99
    }
}
