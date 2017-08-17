using BenzeneSoft.QBuild.Clauses;
using System;
using System.Collections.Generic;
using System.Reflection;
using static BenzeneSoft.QBuild.Functions.SqlFunctions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class FunctionParser
    {
        private static readonly Dictionary<string, string> FunctionNameMap;

        static FunctionParser()
        {
            FunctionNameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
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
        }

        public bool CanParse(string funcName)
        {
            return FunctionNameMap.ContainsKey(funcName);
        }

        public IClause Parse(string funcName, params IClause[] arguments)
        {
            var argClause = new SeparatedClause(new Clause(","));
            foreach (var arg in arguments)
            {
                argClause.AppendSeparated(arg);
            }

            var clause = new MutableClause(argClause).WrapParentheses().Prepend(FunctionNameMap[funcName]);
            return clause;
        }
    }
}
