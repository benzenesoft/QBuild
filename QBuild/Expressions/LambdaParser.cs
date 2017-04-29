using System.Linq.Expressions;
using BenzeneSoft.QBuild.Sqls;

namespace BenzeneSoft.QBuild.Expressions
{
    public class LambdaParser : ILambdaParser
    {
        private readonly IParserLookup _lookup;

        public LambdaParser(IParserLookup lookup)
        {
            _lookup = lookup;
        }

        public ISql Parse(LambdaExpression predicate)
        {
            var expression = predicate.Body;
            var parser = _lookup[expression];
            return parser.Parse(expression);
        }
    }
}