using BenzeneSoft.QBuild.Functions;
using System.Collections.Generic;
using System.Reflection;
using static BenzeneSoft.QBuild.Functions.SqlFunctions;

namespace BenzeneSoft.QBuild.Expressions
{

    public class SqlFunctionNameResolver : ISqlFunctionNameResolver
    {
        private readonly Dictionary<MethodInfo, string> _functionNameMap;

        public SqlFunctionNameResolver()
        {
            var type = typeof(SqlFunctions);
            _functionNameMap = new Dictionary<MethodInfo, string>
            {
                [type.GetMethod(nameof(Avg))] = "avg",
                [type.GetMethod(nameof(Max))] = "max",
                [type.GetMethod(nameof(Min))] = "min",
                [type.GetMethod(nameof(Count))] = "count",
                [type.GetMethod(nameof(Sum))] = "sum",

                [type.GetMethod(nameof(Sqrt))] = "sqrt",
                [type.GetMethod(nameof(Abs))] = "abs",
                [type.GetMethod(nameof(Mod))] = "mod",
                [type.GetMethod(nameof(Floor))] = "floor",
                [type.GetMethod(nameof(Ceil))] = "ceil",
            };
        }

        public string Resolve(MethodInfo method)
        {
            if(!_functionNameMap.TryGetValue(method, out string func))
            {
                _functionNameMap.TryGetValue(Reduce(method), out func);
            }
            return func;
        }

        public SqlFunctionNameResolver AddResolution(MethodInfo methodInfo, string sqlFunction)
        {
            _functionNameMap[methodInfo] = sqlFunction;
            return this;
        }

        public bool CanResolve(MethodInfo method)
        {
            return _functionNameMap.ContainsKey(method)
                || _functionNameMap.ContainsKey(Reduce(method));
        }

        private static MethodInfo Reduce(MethodInfo method)
        {
            if (method.IsGenericMethod)
            {
                method = method.GetGenericMethodDefinition();
            }

            return method;
        }
    }
}
