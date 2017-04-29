using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Expressions;
using BenzeneSoft.QBuild.Sqls;

namespace BenzeneSoft.QBuild.Builders
{
    public class OrderByBuilder : IOrderByBuilder
    {
        private readonly ILambdaParser _lambdaParser;
        private CompositeSql _sql;

        public OrderByBuilder(ILambdaParser lambdaParser)
        {
            _lambdaParser = lambdaParser;
            _sql = new CompositeSql(new Sql().Line().Append(","));
        }

        public ISql Build()
        {
            return _sql;
        }

        public IOrderByBuilder Asc(params string[] orderExpression)
        {
            _sql.AddRange(orderExpression.Select(exp => new Sql($"{exp} ASC")));
            return this;
        }

        public IOrderByBuilder Desc(params string[] orderExpression)
        {
            _sql.AddRange(orderExpression.Select(exp => new Sql($"{exp} DESC")));
            return this;
        }

        public IOrderByBuilder Asc<T>(params Expression<Func<T, object>>[] orderProperty)
        {
            _sql.AddRange(orderProperty.Select(exp => new Sql(_lambdaParser.Parse(exp)).Append(" ASC")));
            return this;
        }

        public IOrderByBuilder Desc<T>(params Expression<Func<T, object>>[] orderProperty)
        {
            _sql.AddRange(orderProperty.Select(exp => new Sql(_lambdaParser.Parse(exp)).Append(" DESC")));
            return this;
        }
    }
}