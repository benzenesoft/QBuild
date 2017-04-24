using System.Collections.Generic;
using System.Linq.Expressions;
using static System.Linq.Expressions.ExpressionType;

namespace BenzeneSoft.QBuild.Expressions
{
    public class BinaryOperationParser : IOperationParser
    {
        private static readonly Dictionary<ExpressionType, string> Operations = new Dictionary<ExpressionType, string>
        {

            { Equal, "="},
            { NotEqual, "<>"},
            { GreaterThan, ">"},
            { GreaterThanOrEqual, ">="},
            { LessThan, "<"},
            { LessThanOrEqual, "<="},

            { OrElse, "OR"},
            { AndAlso, "AND"},

            { Add, "+"},
            { Subtract, "-"},
            { Multiply, "*"},
            { Divide, "/"},
        };

        public bool CanParse(ExpressionType operation)
        {
            return Operations.ContainsKey(operation);
        }

        public ISql Parse(ExpressionType operation, params ISql[] operands)
        {
            var sql = new Sql().Append(operands[0]).Append($" {Operations[operation]} ").Append(operands[1]);
            return sql;
        }
    }
}