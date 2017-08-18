using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Utils;

namespace BenzeneSoft.QBuild.Expressions
{
    public class MethodCallExpressionParser : IExpressionParser
    {
        private readonly IParserLookup _lookup;
        private readonly ISqlFunctionNameResolver _funcResolver;

        public MethodCallExpressionParser(IParserLookup lookup, ISqlFunctionNameResolver funcResolver)
        {
            _lookup = lookup;
            _funcResolver = funcResolver;
        }

        public bool CanParse(Expression expression)
        {
            return expression is MethodCallExpression
                && _funcResolver.CanResolve(((MethodCallExpression)expression).Method);
        }

        public IClause Parse(Expression expression, ClauseContext context)
        {
            return ParseExact((MethodCallExpression)expression, context);
        }

        public IClause ParseExact(MethodCallExpression expression, ClauseContext context)
        {
            var functionName = _funcResolver.Resolve(expression.Method);
            var argClause = new SeparatedClause(",".ToClause());
            foreach (var arg in expression.Arguments)
            {
                argClause.AppendSeparated(_lookup.Parse(arg, context));
            }

            var clause = new MutableClause(argClause).WrapParentheses().PrependText(functionName);
            return clause;
        }
    }
}
