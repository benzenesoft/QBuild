using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.NameResolvers;
using System;
using System.Collections.Generic;
using System.Reflection;
using static BenzeneSoft.QBuild.Functions.SqlFunctions;

namespace BenzeneSoft.QBuild.Expressions
{

    public class SqlFunctionNameResolver: ISqlFunctionNameResolver
    {
        private readonly INameResolver _nameResolver;
        private readonly Dictionary<string, string> _functionNameMap;

        public SqlFunctionNameResolver(INameResolver nameResolver)
        {
            _functionNameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                [nameof(Avg)] = "avg",
                [nameof(Max)] = "max",
                [nameof(Min)] = "min",
                [nameof(Count)] = "count",
                [nameof(Sum)] = "sum",

                [nameof(Sqrt)] = "sum",
                [nameof(Abs)] = "sum",
                [nameof(Mod)] = "sum",
                [nameof(Floor)] = "sum",
                [nameof(Ceil)] = "sum",
            };
            _nameResolver = nameResolver;
        }

        public string Lookup(MethodInfo methodInfo)
        {
            if(!_functionNameMap.TryGetValue(methodInfo.Name, out string func))
            {
                func = _nameResolver.Resolve(methodInfo);
            }

            return func;
        }

        public IClause Parse(string funcName, params IClause[] arguments)
        {
            var argClause = new SeparatedClause(new Clause(","));
            foreach (var arg in arguments)
            {
                argClause.AppendSeparated(arg);
            }

            var clause = new MutableClause(argClause).WrapParentheses().Prepend(_functionNameMap[funcName]);
            return clause;
        }
    }
}
