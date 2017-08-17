using System;
using System.Linq;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.NameResolvers;

namespace BenzeneSoft.QBuild.Expressions
{
    public class ParserLookup : IParserLookup
    {
        private readonly IExpressionParser[] _expressionParsers;
        private readonly IOperationParser[] _operationParsers;

        public ParserLookup(INameResolver nameResolver)
        {
            _expressionParsers = new IExpressionParser[]
            {
                new MethodCallExpressionParser(this),
                new ConstantExpressionParser(),
                new PropertyExpressionParser(nameResolver),
                new NullityExpressionParser(this),
                new BinaryExpressionParser(this),
                new UnaryExpressionParser(this)
            };

            _operationParsers = new IOperationParser[]
            {
                new BinaryOperationParser()
            };
        }

        public IExpressionParser Lookup(Expression expression)
        {
            var parser = _expressionParsers.FirstOrDefault(p => p.CanParse(expression));
            if (parser != null)
            {
                return parser;
            }
            throw new ArgumentException($"Expression ({expression}) of type ({expression.Type}) is not supported.");
        }

        public IOperationParser Lookup(ExpressionType operation)
        {
            var parser = _operationParsers.FirstOrDefault(p => p.CanParse(operation));
            if (parser != null)
            {
                return parser;
            }
            throw new ArgumentException($"Operation {operation} is not supported.");
        }

        public IClause Parse(Expression expression, ClauseContext context)
        {
            var parser = Lookup(expression);
            return parser.Parse(expression, context);
        }

        public IClause Parse(ExpressionType operation, params IClause[] operands)
        {
            var parser = Lookup(operation);
            return parser.Parse(operation, operands);
        }
    }
}