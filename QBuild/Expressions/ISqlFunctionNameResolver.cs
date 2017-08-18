using System.Reflection;

namespace BenzeneSoft.QBuild.Expressions
{
    public interface ISqlFunctionNameResolver
    {
        string Resolve(MethodInfo method);
        bool CanResolve(MethodInfo method);
    }
}
