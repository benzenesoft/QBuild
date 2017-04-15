using System;

namespace BenzeneSoft.SqlBuilder
{
    public interface IQueryBuilder : ISqlBuilder
    {
        IQueryBuilder Select(Action<ISelectBuilder> build);
        IQueryBuilder From(string expression);
        IQueryBuilder Where(Action<IWhereBuilder, IPredicateFactory> builder);
    }

    public interface IQueryBuilder<T> : IQueryBuilder
    {
        IQueryBuilder<T> Select(Action<ISelectBuilder<T>> build);
        IQueryBuilder<T> Where(Action<IWhereBuilder, IPredicateFactory<T>> builder);
    }
}