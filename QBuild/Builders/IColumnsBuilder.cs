using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Builders
{
    // TODO feature: alias (x AS y)
    public interface IColumnsBuilder : ISqlBuilder
    {
        IColumnsBuilder All();
        IColumnsBuilder Columns(params string[] expressions);
    }

    public interface IColumnsBuilder<T> : IColumnsBuilder
    {
        IColumnsBuilder<T> Columns(params Expression<Func<T, object>>[] expressions);
    }
}