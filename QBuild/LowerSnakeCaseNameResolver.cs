using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BenzeneSoft.QBuild
{
    public class LowerSnakeCaseNameResolver : INameResolver
    {
        public string Table(Type type)
        {
            return ToSnakeCase(type.Name);
        }

        public string Column(PropertyInfo prop)
        {
            return ToSnakeCase(prop.Name);
        }

        public string Column<T>(Expression<Func<T, object>> prop)
        {
            var pInfo = GetPropertyFromExpression(prop);
            return Column(pInfo);
        }

        private string ToSnakeCase(string input)
        {
            var snake = string.Concat(input.Skip(1).Select(c => char.IsUpper(c) ? "_" + char.ToLower(c) : c.ToString()));
            snake = char.ToLower(input[0]) + snake;
            return snake;
        }

        private PropertyInfo GetPropertyFromExpression<T>(Expression<Func<T, object>> expression)
        {
            MemberExpression exp;

            if (expression.Body is UnaryExpression)
            {
                var unExp = (UnaryExpression)expression.Body;
                if (unExp.Operand is MemberExpression)
                {
                    exp = (MemberExpression) unExp.Operand;
                }
                else
                {
                    throw new ArgumentException("Expression must be a property");
                }
            }
            else if (expression.Body is MemberExpression)
            {
                exp = (MemberExpression)expression.Body;
            }
            else
            {
                throw new ArgumentException("Expression must be a property");
            }

            return (PropertyInfo)exp.Member;
        }

    }
}