using System;

namespace MathParser
{
    public static class ParserResultExtentions
    {
        public static ParserResult Select(this string source, Func<string, ParserResult> selector)
        {
            return selector(source);
        }

        public static ParserResult Select(this ParserResult source, Func<ParserResult, ParserResult> selector)
        {
            return selector(source);
        }

        public static TResult SelectMany<TSource, TCollection, TResult>(this TSource source,
                                                                        Func<TSource, TCollection> selector,
                                                                        Func<TSource, TCollection, TResult> projector)
        {
            TCollection u = selector(source);
            return projector(source, u);
        }

        public static ParserResult OR(this ParserResult parser1, ParserResult parser2)
        {
            var parserResult = new ParserResult();
            if (parser1 != null)
            {
                if (parser1.Output == null && parser2.Output == null)
                {
                    //return null;
                    parserResult.Input = parser1.Input;
                    parserResult.Output = parser1.Output;
                    parserResult.ExpressionType = parser1.ExpressionType;
                }
                else if (parser1.Output == null)
                {
                    parserResult.Input = parser2.Input;
                    parserResult.Output = parser2.Output;
                    parserResult.ExpressionType = parser2.ExpressionType;
                }
                else
                {
                    parserResult.Input = parser1.Input;
                    parserResult.Output = parser1.Output;
                    parserResult.ExpressionType = parser1.ExpressionType;
                }
            }

            return parserResult;
        }

        //public static ParserFunction AND(this ParserFunction parser1, ParserFunction parser2)
        //{
        //    return input =>
        //               {
        //                   var p1 = parser1(input);
        //                   return p1 != null ? parser2(p1) : null;
        //               };
        //}
    }
}