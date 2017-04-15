using System;
using System.Linq.Expressions;

namespace BenzeneSoft.SqlBuilder
{
    public class PredicateFactory<T> : IPredicateFactory<T>
    {
        private readonly INameResolver _nameResolver;

        public PredicateFactory(INameResolver nameResolver)
        {
            _nameResolver = nameResolver;
        }

        public ISql Binary(string leftExpression, string comparison, object rightValue)
        {
            var param = Parameter.CreateNew(rightValue);
            return new Sql($"{leftExpression} {comparison} {param.Name}", param);
        }

        public ISql Binary(string leftExpression, string comparison, string rightExpression)
        {
            return new Sql($"{leftExpression} {comparison} {rightExpression}");
        }

        public ISql Binary(Expression<Func<T, object>> leftExpression, string comparison, object rightValue)
        {
            var lx = _nameResolver.Column(leftExpression);
            return Binary(lx, comparison, rightValue);
        }

        public ISql Binary(Expression<Func<T, object>> leftExpression, string comparison, Expression<Func<T, object>> rightExpression)
        {
            var lx = _nameResolver.Column(leftExpression);
            var rx = _nameResolver.Column(rightExpression);
            return Binary(lx, comparison, rx);
        }

        public ISql Or(params ISql[] expressions)
        {
            return Join(" OR ", expressions);
        }

        public ISql And(params ISql[] expressions)
        {
            return Join(" AND ", expressions);
        }

        public ISql Join(string separator, ISql[] expressions)
        {
            var sql = new Sql();

            sql.Append(expressions[0], true);
            for (var i = 1; i < expressions.Length; i++)
            {
                sql.Text(separator).Append(expressions[i], true);
            }

            return sql;
        }
    }
}