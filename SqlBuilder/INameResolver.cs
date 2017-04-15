using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BenzeneSoft.SqlBuilder
{
    public interface INameResolver
    {
        string Table(Type type);
        string Column(PropertyInfo prop);
        string Column<T>(Expression<Func<T, object>> prop);
    }
}
