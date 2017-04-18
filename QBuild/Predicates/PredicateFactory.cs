using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BenzeneSoft.QBuild.Predicates
{
    public class PredicateFactory<T> : IPredicateFactory<T>
    {
        private static readonly OperatorMap Map = new OperatorMap();
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

            sql.Append(expressions[0], wrapParanthesis: true);
            for (var i = 1; i < expressions.Length; i++)
            {
                sql.Append(separator).Append(expressions[i], wrapParanthesis: true);
            }

            return sql;
        }

        public ISql Expression(Expression<Predicate<T>> expression)
        {
            var bin = expression.Body as BinaryExpression;
            if (bin == null)
            {
                throw new ArgumentException("expression must be a binary expression");
            }

            var left = bin.Left as MemberExpression;

            if (left == null)
            {
                throw new ArgumentException("left side of the expression must be a property");
            }

            var leftProp = left.Member as PropertyInfo;

            if (leftProp == null)
            {
                throw new ArgumentException("left side of the expression must be a property");
            }

            var leftColumn = _nameResolver.Column(leftProp);

            var op = Map[bin.NodeType];
            var right = bin.Right;


            if (right is ConstantExpression)
            {
                var rightValue = (right as ConstantExpression).Value;
                return Binary(leftColumn, "=", rightValue);
            }
            if (right is MemberExpression)
            {
                var rightProp = (right as MemberExpression).Member as PropertyInfo;
                return Binary(leftColumn, op, _nameResolver.Column(rightProp));
            }

            throw new ArgumentException("right side of the expression must be a property or a constant value");
        }
    }
}