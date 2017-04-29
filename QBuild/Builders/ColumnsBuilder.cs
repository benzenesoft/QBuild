using System;
using System.Linq;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Expressions;
using BenzeneSoft.QBuild.Sqls;

namespace BenzeneSoft.QBuild.Builders
{
    public class ColumnsBuilder : IColumnsBuilder
    {
        private readonly ILambdaParser _lambdaParser;
        private CompositeSql _sql;

        public ColumnsBuilder(ILambdaParser lambdaParser)
        {
            _lambdaParser = lambdaParser;
            _sql = new CompositeSql(new Sql().Line().Append(","));
        }

        public ISql Build()
        {
            return _sql;
        }

        public IColumnsBuilder All()
        {
            _sql.Add("*");
            return this;
        }

        public IColumnsBuilder Columns(params string[] expressions)
        {
            _sql.AddRange(expressions.Select(exp => new Sql(exp)));
            return this;
        }

        public IColumnsBuilder Columns<T>(params Expression<Func<T, object>>[] expressions)
        {
            _sql.AddRange(expressions.Select(_lambdaParser.Parse));
            return this;
        }
    }
}