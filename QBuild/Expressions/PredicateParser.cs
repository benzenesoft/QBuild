using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class PredicateParser : IPredicateParser
    {
        private readonly IParserLookup _lookup;

        public PredicateParser(IParserLookup lookup)
        {
            _lookup = lookup;
        }

        public ISql Parse<T>(Expression<Func<T, bool>> predicate)
        {
            var expression = predicate.Body;
            var parser = _lookup[expression];
            return parser.Parse(expression);
        }
    }
}