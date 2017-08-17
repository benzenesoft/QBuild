using System.Reflection;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface ISqlFunctionNameResolver
    {
        string Lookup(MethodInfo methodInfo);
    }
}
