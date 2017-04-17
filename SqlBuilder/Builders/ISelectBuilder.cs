using System;
using System.Linq.Expressions;

namespace BenzeneSoft.SqlBuilder.Builders
{
    public interface ISelectBuilder : ISqlBuilder
    {
        ISelectBuilder All();
        ISelectBuilder Columns(params string[] expressions);
    }

    public interface ISelectBuilder<T> : ISelectBuilder
    {
        ISelectBuilder<T> Columns(params Expression<Func<T, object>>[] expressions);
    }
}