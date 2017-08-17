using System.Collections.Generic;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Expressions
{
    public class LambdaResolver : ILambdaResolver
    {
        private readonly IParserLookup _lookup;

        public LambdaResolver(IParserLookup lookup)
        {
            _lookup = lookup;
        }

        public IClause Parse(LambdaExpression expression, ClauseContext context)
        {
            return _lookup.Parse(expression.Body, context);
        }
    }
}