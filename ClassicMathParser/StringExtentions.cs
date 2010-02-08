using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ClassicMathParser
{
    public static class StringExtentions
    {
        public static double ToDouble(this string value)
        {
            double result = 0;
            double.TryParse(value, NumberStyles.Float, new CultureInfo("en-GB"), out result);
            return result;
        }
    }
}
