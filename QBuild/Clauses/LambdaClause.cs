using System.Collections.Generic;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Expressions;

namespace BenzeneSoft.QBuild.Clauses
{
    public class LambdaClause : IClause
    {
        private readonly ILambdaResolver _parser;
        private IClause _clause;

        public LambdaClause(ILambdaResolver parser)
        {
            _parser = parser;
        }

        public LambdaClause Expression(LambdaExpression expression, ClauseContext context)
        {
            _clause = _parser.Parse(expression, context);
            return this;
        }

        public string Text => _clause.Text;
        public IEnumerable<Parameter> Parameters => _clause.Parameters;
        public bool IsEmpty => _clause.IsEmpty;
    }
}
