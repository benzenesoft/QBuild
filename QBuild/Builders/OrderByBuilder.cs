using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Builders
{
    public class OrderByBuilder : IOrderByBuilder
    {
        private readonly INameResolver _nameResolver;
        private readonly List<string> _expressions;

        public OrderByBuilder(INameResolver nameResolver)
        {
            _nameResolver = nameResolver;
            _expressions = new List<string>();
        }

        public ISql Build()
        {
            var sql = new Sql();
            if (_expressions.Count > 0)
            {
                sql.Append("ORDER BY ").Append(_expressions[0]);
                for (var i = 1; i < _expressions.Count; i++)
                {
                    sql.Line().Append(",").Append(_expressions[i]);
                }
            }
            return sql;
        }

        public IOrderByBuilder Asc(params string[] orderExpression)
        {
            _expressions.AddRange(orderExpression.Select(exp => $"{exp} ASC"));
            return this;
        }

        public IOrderByBuilder Desc(params string[] orderExpression)
        {
            _expressions.AddRange(orderExpression.Select(exp => $"{exp} DESC"));
            return this;
        }

        public IOrderByBuilder Asc<T>(params Expression<Func<T, object>>[] orderProperty)
        {
            return Asc(orderProperty.Select(_nameResolver.Column).ToArray());
        }

        public IOrderByBuilder Desc<T>(params Expression<Func<T, object>>[] orderProperty)
        {
            return Desc(orderProperty.Select(_nameResolver.Column).ToArray());
        }
    }
}