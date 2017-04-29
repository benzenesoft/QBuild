using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Builders
{
    // TODO feature: alias (x AS y)
    public interface IColumnsBuilder : ISqlBuilder
    {
        IColumnsBuilder All();
        IColumnsBuilder Columns(params string[] expressions);
        IColumnsBuilder Columns<T>(params Expression<Func<T, object>>[] expressions);
    }
}