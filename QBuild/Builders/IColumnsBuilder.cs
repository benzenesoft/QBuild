using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Builders
{
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