using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Builders
{
    public interface IOrderByBuilder : ISqlBuilder
    {
        IOrderByBuilder Asc(params string[] orderExpression);
        IOrderByBuilder Desc(params string[] orderExpression);
        IOrderByBuilder Asc<T>(params Expression<Func<T, object>>[] orderProperty);
        IOrderByBuilder Desc<T>(params Expression<Func<T, object>>[] orderProperty);
    }
}
