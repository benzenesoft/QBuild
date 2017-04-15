using System;

namespace BenzeneSoft.SqlBuilder
{
    public class QueryBuilder<T> : IQueryBuilder<T>
    {
        private readonly ISelectBuilder<T> _selectBuilder;

        public QueryBuilder(ISelectBuilder<T> selectBuilder)
        {
            _selectBuilder = selectBuilder;
        }

        public ISql Build()
        {
            var sql = new Sql();
            sql.Append(_selectBuilder.Build()).Line();

            return sql;
        }

        public IQueryBuilder Select(Action<ISelectBuilder> build)
        {
            build.Invoke(_selectBuilder);
            return this;
        }

        public IQueryBuilder<T> Select(Action<ISelectBuilder<T>> build)
        {
            build.Invoke(_selectBuilder);
            return this;
        }

        public IQueryBuilder From(string expression)
        {
            throw new NotImplementedException();
        }

        public IQueryBuilder Where(Action<IWhereBuilder, IPredicateFactory> builder)
        {
            throw new NotImplementedException();
        }

        public IQueryBuilder<T> Where(Action<IWhereBuilder, IPredicateFactory<T>> builder)
        {
            throw new NotImplementedException();
        }
    }
}