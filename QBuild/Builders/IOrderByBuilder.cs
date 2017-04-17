using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Builders
{
    public interface IOrderByBuilder : ISqlBuilder
    {
        IOrderByBuilder Asc(params string[] orderExpression);
        IOrderByBuilder Desc(params string[] orderExpression);
    }

    public interface IOrderByBuilder<T> : IOrderByBuilder
    {
        IOrderByBuilder<T> Asc(params Expression<Func<T, object>>[] orderProperty);
        IOrderByBuilder<T> Desc(params Expression<Func<T, object>>[] orderProperty);
    }
}
