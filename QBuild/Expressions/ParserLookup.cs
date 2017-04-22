using System;
using System.Linq;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class ParserLookup : IParserLookup
    {
        private IExpressionParser[] _prioritisedParsers;

        public ParserLookup(INameResolver nameResolver, IOperatorResolver operatorResolver)
        {
            var constantParser = new ConstantExpressionParser();
            var propertyParser = new PropertyExpressionParser(nameResolver);
            var binaryParser = new BinaryExpressionParser(operatorResolver, constantParser, propertyParser);
            var notExpressionParser = new NotExpressionParser(nameResolver);
            _prioritisedParsers = new IExpressionParser[]
            {
                constantParser, propertyParser, binaryParser, notExpressionParser
            };
        }

        public IExpressionParser FindParser(Expression expression)
        {
            var parser = _prioritisedParsers.FirstOrDefault(p => p.CanParse(expression));
            if (parser != null)
            {
                return parser;
            }
            throw new ArgumentException($"Expression ({expression}) of type ({expression.Type}) is not supported.");
        }
    }
}