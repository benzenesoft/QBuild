using System.Linq;
using System.Reflection;

namespace BenzeneSoft.QBuild.NameResolvers
{
    public class LowerSnakeCaseNameResolver : INameResolver
    {
        public static string ToSnakeCase(string input)
        {
            var snake = string.Concat(input.Skip(1).Select(c => char.IsUpper(c) ? "_" + char.ToLower(c) : c.ToString()));
            snake = char.ToLower(input[0]) + snake;
            return snake;
        }

        public string Resolve(MemberInfo memberInfo)
        {
            return ToSnakeCase(memberInfo.Name);
        }
    }
}