using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BenzeneSoft.QBuild.Expressions
{
    public class PropertyExpressionParser : IExpressionParser
    {
        private readonly INameResolver _nameResolver;

        public PropertyExpressionParser(INameResolver nameResolver)
        {
            _nameResolver = nameResolver;
        }

        public ISql Parse(Expression expression)
        {
            return ParseExact(expression as MemberExpression);
        }

        private ISql ParseExact(MemberExpression expression)
        {
            var prop = expression.Member as PropertyInfo;
            var column = _nameResolver.Column(prop);
            return new Sql(column);
        }

        public bool CanParse(Expression expression)
        {
            return (expression as MemberExpression)?.Member is PropertyInfo;
        }
    }
}