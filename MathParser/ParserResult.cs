using System.Linq.Expressions;

namespace MathParser
{
    public class ParserResult
    {
        public string Input { get; set; }
        public Expression Output { get; set; }
        public ExpressionType ExpressionType { get; set; }
    }
}