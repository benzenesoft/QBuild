using System.Linq.Expressions;
using System.Reflection;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.NameResolvers;
using BenzeneSoft.QBuild.Utils;

namespace BenzeneSoft.QBuild.Expressions
{
    public class PropertyExpressionParser : IExpressionParser
    {
        private readonly INameResolver _nameResolver;

        public PropertyExpressionParser(INameResolver nameResolver)
        {
            _nameResolver = nameResolver;
        }

        public IClause Parse(Expression expression, ClauseContext context)
        {
            return ParseExact(expression as MemberExpression);
        }

        private IClause ParseExact(MemberExpression expression)
        {
            var prop = expression.Member as PropertyInfo;
            var column = _nameResolver.Resolve(prop);
            return column.ToClause();
        }

        public bool CanParse(Expression expression)
        {
            var can =  (expression as MemberExpression)?.Member is PropertyInfo;

            return can;
        }
    }
}