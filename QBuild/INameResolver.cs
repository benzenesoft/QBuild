using System;
using System.Reflection;

namespace BenzeneSoft.QBuild
{
    public interface INameResolver
    {
        string Table(Type type);
        string Column(PropertyInfo prop);
    }
}
