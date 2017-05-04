using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Expressions;

namespace BenzeneSoft.QBuild.Builders
{
    public class OrderByBuilder : IOrderByBuilder
    {
        private readonly ILambdaParser _lambdaParser;
        private CompositeClause _clause;

        public OrderByBuilder(ILambdaParser lambdaParser)
        {
            _lambdaParser = lambdaParser;
            _clause = new CompositeClause(new Clause().Line().Append(","));
        }

        public IClause Build()
        {
            return _clause;
        }

        public IOrderByBuilder Asc(params string[] orderExpression)
        {
            _clause.AddRange(orderExpression.Select(exp => new Clause($"{exp} ASC")));
            return this;
        }

        public IOrderByBuilder Desc(params string[] orderExpression)
        {
            _clause.AddRange(orderExpression.Select(exp => new Clause($"{exp} DESC")));
            return this;
        }

        public IOrderByBuilder Asc<T>(params Expression<Func<T, object>>[] orderProperty)
        {
            _clause.AddRange(orderProperty.Select(exp => new Clause(_lambdaParser.Parse(exp)).Append(" ASC")));
            return this;
        }

        public IOrderByBuilder Desc<T>(params Expression<Func<T, object>>[] orderProperty)
        {
            _clause.AddRange(orderProperty.Select(exp => new Clause(_lambdaParser.Parse(exp)).Append(" DESC")));
            return this;
        }
    }
}