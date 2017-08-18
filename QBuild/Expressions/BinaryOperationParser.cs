using System.Collections.Generic;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;
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

        public IClause Parse(ExpressionType operation, params IClause[] operands)
        {
            var clause = new MutableClause().Append(operands[0]).AppendText($" {Operations[operation]} ").Append(operands[1]);
            return clause;
        }
    }
}