using System;
using System.Reflection;

namespace BenzeneSoft.QBuild.NameResolvers
{
    public interface INameResolver
    {
        string Table(Type type);
        string Column(PropertyInfo prop);
    }
}
