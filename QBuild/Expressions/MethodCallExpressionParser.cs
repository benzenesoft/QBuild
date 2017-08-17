using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Functions;
using System.Linq;

namespace BenzeneSoft.QBuild.Expressions
{
    public class MethodCallExpressionParser : IExpressionParser
    {
        private readonly IParserLookup _lookup;
        private readonly FunctionParser _functParser = new FunctionParser();

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
            return ParseExact((MethodCallExpression)expression, context);
        }

        public IClause ParseExact(MethodCallExpression expression, ClauseContext context)
        {
            var method = expression.Method;
            var arguments = expression.Arguments.Select(arg=> _lookup.Parse(arg, context)).ToArray();
            return _functParser.Parse(method.Name, arguments);
        }
    }
}
