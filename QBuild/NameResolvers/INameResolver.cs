using System.Reflection;

namespace BenzeneSoft.QBuild.NameResolvers
{
    public interface INameResolver
    {
        string Resolve(MemberInfo memberInfo);
    }
}
