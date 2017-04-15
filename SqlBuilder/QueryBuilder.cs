using System;

namespace BenzeneSoft.SqlBuilder
{
    public class QueryBuilder<T> : IQueryBuilder<T>
    {
        private readonly ISelectBuilder<T> _selectBuilder;
        private readonly IFromBuilder<T> _fromBuilder;
        private readonly IWhereBuilder _whereBuilder;
        private readonly IPredicateFactory<T> _predicateFactory;

        public QueryBuilder(ISelectBuilder<T> selectBuilder, IFromBuilder<T> fromBuilder
            , IWhereBuilder whereBuilder, IPredicateFactory<T> predicateFactory)
        {
            _selectBuilder = selectBuilder;
            _fromBuilder = fromBuilder;
            _whereBuilder = whereBuilder;
            _predicateFactory = predicateFactory;
        }

        public ISql Build()
        {
            var sql = new Sql()
                .Append(_selectBuilder.Build()).Line()
                .Append(_fromBuilder.Build()).Line()
                .Append(_whereBuilder.Build()).Line();

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

        public IQueryBuilder From(Action<IFromBuilder> build)
        {
            build.Invoke(_fromBuilder);
            return this;
        }

        public IQueryBuilder From(Action<IFromBuilder<T>> build)
        {
            build.Invoke(_fromBuilder);
            return this;
        }

        public IQueryBuilder Where(Action<IWhereBuilder, IPredicateFactory> build)
        {
            build.Invoke(_whereBuilder, _predicateFactory);
            return this;
        }

        public IQueryBuilder<T> Where(Action<IWhereBuilder, IPredicateFactory<T>> build)
        {
            build.Invoke(_whereBuilder, _predicateFactory);
            return this;
        }
    }
}