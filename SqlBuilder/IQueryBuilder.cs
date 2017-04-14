using System;

namespace BenzeneSoft.SqlBuilder
{
    public interface IQueryBuilder : ISqlBuilder
    {
        IQueryBuilder Select(Action<ISelectBuilder> builder);
        IQueryBuilder From(string expression);
        IQueryBuilder Where(Action<IWhereBuilder, IPredicateFactory> builder);
    }

    public interface IQueryBuilder<out T> : IQueryBuilder
    {
        IQueryBuilder<T> Select(Action<ISelectBuilder<T>> builder);
        IQueryBuilder<T> Where(Action<IWhereBuilder, IPredicateFactory<T>> builder);
    }
}