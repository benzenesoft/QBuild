using System;
using System.Linq;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Expressions;

namespace BenzeneSoft.QBuild.Builders
{
    public class ColumnsBuilder : IColumnsBuilder
    {
        private readonly ILambdaParser _lambdaParser;
        private CompositeClause _clause;

        public ColumnsBuilder(ILambdaParser lambdaParser)
        {
            _lambdaParser = lambdaParser;
            _clause = new CompositeClause(new MutableClause().Line().Append(","));
        }

        public IClause Build()
        {
            return _clause;
        }

        public IColumnsBuilder All()
        {
            _clause.Add("*");
            return this;
        }

        public IColumnsBuilder Columns(params string[] expressions)
        {
            _clause.AddRange(expressions.Select(exp => new MutableClause(exp)));
            return this;
        }

        public IColumnsBuilder Columns<T>(params Expression<Func<T, object>>[] expressions)
        {
            _clause.AddRange(expressions.Select(_lambdaParser.Parse));
            return this;
        }
    }
}