using System.Collections.Generic;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Expressions;

namespace BenzeneSoft.QBuild.Clauses
{
    public class LambdaClause : IClause
    {
        private readonly ILambdaParser _parser;
        private readonly LambdaExpression _expression;
        private IClause _clause;
        public LambdaClause(ILambdaParser parser, LambdaExpression expression)
        {
            _parser = parser;
            _expression = expression;
        }

        public IClause Clause => _clause ?? (_clause = _parser.Parse(_expression));

        public string Text => Clause.Text;
        public IEnumerable<Parameter> Parameters => Clause.Parameters;
    }
}
