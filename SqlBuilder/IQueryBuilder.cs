using System;

namespace BenzeneSoft.SqlBuilder
{
    public interface IQueryBuilder : ISqlBuilder
    {
        IQueryBuilder Select(Action<ISelectBuilder> build);
        IQueryBuilder From(Action<IFromBuilder> build);
        IQueryBuilder Where(Action<IWhereBuilder, IPredicateFactory> build);
    }

    public interface IQueryBuilder<T> : IQueryBuilder
    {
        IQueryBuilder<T> Select(Action<ISelectBuilder<T>> build);
        IQueryBuilder From(Action<IFromBuilder<T>> build);
        IQueryBuilder<T> Where(Action<IWhereBuilder, IPredicateFactory<T>> build);
    }
}