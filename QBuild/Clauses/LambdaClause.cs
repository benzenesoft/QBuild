using System.Collections.Generic;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Expressions;

namespace BenzeneSoft.QBuild.Clauses
{
    public class LambdaClause : IClause
    {
        private readonly ILambdaParser _parser;
        private IClause _clause;

        public LambdaClause(ILambdaParser parser)
        {
            _parser = parser;
        }

        public LambdaClause Expression(LambdaExpression expression)
        {
            _clause = _parser.Parse(expression);
            return this;
        }

        public string Text => _clause.Text;
        public IEnumerable<Parameter> Parameters => _clause.Parameters;
    }
}
