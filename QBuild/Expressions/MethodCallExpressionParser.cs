using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;

namespace BenzeneSoft.QBuild.Expressions
{
    public class MethodCallExpressionParser : IExpressionParser
    {
        private readonly IParserLookup _lookup;

        public MethodCallExpressionParser(IParserLookup lookup)
        {
            _lookup = lookup;
        }

        public bool CanParse(Expression expression)
        {
            return expression is MethodCallExpression;
        }

        public IClause Parse(Expression expression, ClauseContext context)
        {
            return ParseExact((MethodCallExpression)expression);
        }

        public IClause ParseExact(MethodCallExpression expression)
        {
            var method = expression.Method;
            var arguments = expression.Arguments;
            throw new NotImplementedException();
        }
    }
}
