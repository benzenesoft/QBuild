using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BenzeneSoft.QBuild.Predicates
{
    public class BinaryPredicateExpressionParser : IPredicateExpressionParser
    {
        private readonly INameResolver _nameResolver;
        private readonly IOperatorResolver _operatorResolver;

        public BinaryPredicateExpressionParser(INameResolver nameResolver, IOperatorResolver operatorResolver)
        {
            _nameResolver = nameResolver;
            _operatorResolver = operatorResolver;
        }

        public ISql Parse<T>(Expression<Predicate<T>> expression)
        {
            var bin = expression.Body as BinaryExpression;
            if (bin == null)
            {
                throw new ArgumentException("expression must be a binary expression");
            }
            var left = AsColumnOrValue(bin.Left);
            var op = _operatorResolver[bin.NodeType];
            var right = AsColumnOrValue(bin.Right);

            var sql = new Sql().Append(left).Append($" {op} ").Append(right);

            return sql;
        }

        private ISql AsColumnOrValue(Expression expression)
        {
            if (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression) expression;
                var prop = memberExpression.Member as PropertyInfo;
                if (prop == null)
                {
                    throw new ArgumentException("member expression must be a property");
                }

                var column = _nameResolver.Column(prop);
                return new Sql(column);
            }
            if (expression is ConstantExpression)
            {
                var value = ((ConstantExpression) expression).Value;
                var param = Parameter.CreateNew(value);
                return new Sql(param.Name, param);
            }

            throw new AggregateException("expression must be a property on constant");
        }
    }
}