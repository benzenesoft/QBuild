using System;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class BinaryExpressionParser : IExpressionParser
    {
        private readonly IParserLookup _lookup;

        public BinaryExpressionParser(IParserLookup lookup)
        {
            _lookup = lookup;
        }

        public ISql Parse(Expression expression)
        {
            return ParseExact(expression as BinaryExpression);
        }

        private ISql ParseExact(BinaryExpression expression)
        {
            var left = _lookup[expression.Left].Parse(expression.Left);
            var op = _lookup[expression.NodeType];
            var right = _lookup[expression.Right].Parse(expression.Right);

            var sql = op.Parse(expression.NodeType, left, right);

            return sql;
        }

        public bool CanParse(Expression expression)
        {
            return expression is BinaryExpression;
        }
    }
}