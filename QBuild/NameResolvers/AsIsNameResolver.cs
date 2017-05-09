using System.Reflection;

namespace BenzeneSoft.QBuild.NameResolvers
{
    public class AsIsNameResolver : INameResolver
    {
        public string Resolve(MemberInfo memberInfo)
        {
            return memberInfo.Name;
        }
    }
}