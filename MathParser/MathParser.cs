using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Globalization;

namespace MathParser
{
    public class MathParser
    {
        private ParserFunction digit = input =>
        {
            var rest = input.Input;
            var val = String.Empty;
            while (!string.IsNullOrEmpty(rest) && (Char.IsNumber(rest[0]) || rest[0] == '.'))
            {
                val += rest[0];
                rest = rest.Substring(1);
            }
            if (String.IsNullOrEmpty(val)) // can't parse
                return new ParserResult() { Input = input.Input, ExpressionType = ExpressionType.Quote };

            var constant = double.Parse(val, new CultureInfo("en-GB"));
            return new ParserResult()
                       {
                           Input = rest,
                           ExpressionType = ExpressionType.Constant,
                           Output = Expression.Constant(constant)
                       };
        };

        private ParserFunction mul = input =>
        {
            var rest = input.Input;
            var val = String.Empty;
            while (rest.Length > 0 && rest[0] == '*')
            {
                val += rest[0];
                rest = rest.Substring(1);
            }
            if (String.IsNullOrEmpty(val)) // can 't parse
                return new ParserResult() { Input = input.Input, ExpressionType = ExpressionType.Quote };

            return new ParserResult()
            {
                Input = rest,
                ExpressionType = ExpressionType.Multiply,
                Output = input.Output
            };
        };

        private ParserFunction add = input =>
        {
            var rest = input.Input;
            var val = String.Empty;
            while (rest.Length > 0 && rest[0] == '+')
            {
                val += rest[0];
                rest = rest.Substring(1);
            }
            if (String.IsNullOrEmpty(val)) //can't parse
                return new ParserResult() { Input = input.Input, ExpressionType = ExpressionType.Quote };
            return new ParserResult()
            {
                Input = rest,
                ExpressionType = ExpressionType.Add,
                Output = input.Output

            };
        };

        internal Expression Parse(string s)
        {
            var z = new ParserResult() { Input = s, Output = null };

            /*
            * number     = {"0"|"1"|"2"|"3"|"4"|"5"|"6"|"7"|"8"|"9"}
            * factor     = number | "(" expression ")"
            * component  = factor [{("*" | "/") factor}]
            * expression = component [{("+" | "-") component}]
            */
            ParserFunction expression = null;
            ParserFunction number = null;
            ParserFunction factor = null;
            ParserFunction component = null;

            number = input => digit(input);


            factor = input => from i in number(input)
                              select i;

            component = input => (from i in factor(input)
                                  from k in mul(i)
                                  from o in factor(k)
                                  select k.ExpressionType == ExpressionType.Multiply
                                             ? new ParserResult()
                                                   {
                                                       ExpressionType = k.ExpressionType,
                                                       Input = o.Input,
                                                       Output = o.Output != null && i.Output != null
                                                                    ?
                                                                        Expression.MakeBinary(k.ExpressionType,
                                                                                              i.Output,
                                                                                              o.Output)
                                                                    : null
                                                   }
                                             : new ParserResult()
                                                   {
                                                       Input = input.Input,
                                                       Output = null,
                                                       ExpressionType = ExpressionType.Quote
                                                   }

                                          )
                                     .OR
                                     (
                                         from k in mul(input)
                                         from o in factor(k)
                                         select k.ExpressionType == ExpressionType.Multiply
                                             ?
                                             new ParserResult()
                                                 {
                                                     ExpressionType = k.ExpressionType,
                                                     Input = o.Input,
                                                     Output = o.Output != null && input.Output != null
                                                                  ?
                                                                      Expression.MakeBinary(k.ExpressionType,
                                                                                            input.Output,
                                                                                            o.Output)
                                                                  : null
                                                 } : new ParserResult()
                                                 {
                                                     Input = input.Input,
                                                     Output = null,
                                                     ExpressionType = ExpressionType.Quote
                                                 }
                                     )
                                     .OR
                                     (
                                         from u in factor(input)
                                         select u
                                     )
                                 .OR
                                 (
                                     from u in input
                                     select u
                                 );



            expression = input => (from i in component(input)
                                   from k in add(i)
                                   from o in component(k)
                                   select new ParserResult()
                                              {
                                                  ExpressionType = k.ExpressionType,
                                                  Input = o.Input,
                                                  Output = o.Output != null && i.Output != null
                                                  ? Expression.MakeBinary(k.ExpressionType, i.Output,
                                                                            o.Output)
                                                  : null
                                              })
             .OR
             (
               from i in component(input)
               select i
             )
            .OR
            (
               from i in input
               select i
            );
            var all = from o in Repeat(expression, z)
                      select o;
            return all.Output;


        }

        public static ParserResult Repeat(ParserFunction function, ParserResult parser)
        {
            var result = function(parser);
            if (!String.IsNullOrEmpty(result.Input))
                return Repeat(function, result);
            return result;
        }
    }
}
