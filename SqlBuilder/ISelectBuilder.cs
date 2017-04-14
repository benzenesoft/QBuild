using System;

namespace BenzeneSoft.SqlBuilder
{
    public interface ISelectBuilder : ISqlBuilder
    {
        ISelectBuilder All();
        ISelectBuilder Columns(params string[] expressions);
        ISelectBuilder Custom(ISql sql);
    }

    public interface ISelectBuilder<out T> : ISelectBuilder
    {
        ISelectBuilder<T> Columns(params Func<T, object>[] expressions);
    }
}