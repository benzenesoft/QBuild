using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Functions;

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
            return ParseExact((MethodCallExpression)expression, context);
        }

        public IClause ParseExact(MethodCallExpression expression, ClauseContext context)
        {
            var method = expression.Method;
            if (method.IsGenericMethod)
            {
                method = method.GetGenericMethodDefinition();
            }
            var arguments = expression.Arguments;

            if (method == typeof(FunctionFactory).GetMethod("Avg"))
            {
                var argClause = _lookup[arguments[0]].Parse(arguments[0], context);
                var funcClause = new MutableClause(argClause).WrapParentheses().Prepend("avg");
                return funcClause;
            }

            throw new NotImplementedException();
        }
    }
}
