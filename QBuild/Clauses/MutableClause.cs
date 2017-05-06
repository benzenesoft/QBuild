using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenzeneSoft.QBuild.Builders;

namespace BenzeneSoft.QBuild.Clauses
{
    public class MutableClause : IClause
    {
        private readonly StringBuilder _sqlTextBuilder;
        private readonly List<Parameter> _parameters;

        public MutableClause(string sqlText, params Parameter[] parameters)
        {
            _sqlTextBuilder = new StringBuilder(sqlText);
            _parameters = new List<Parameter>(parameters);
        }

        public MutableClause() : this(string.Empty) { }
        public MutableClause(IClause clause) : this(clause.Text, clause.Parameters.ToArray()) { }
        public MutableClause(IClauseBuilder builder) : this(builder.Build()) { }

        public string Text => _sqlTextBuilder.ToString();

        public IEnumerable<Parameter> Parameters => _parameters;

        public MutableClause Append(IClause clause)
        {
            if (clause == null) return this;

            var text = clause.Text;

            _sqlTextBuilder.Append(text);
            _parameters.AddRange(clause.Parameters);

            return this;
        }
        
        public MutableClause Append(string sqlText)
        {
            _sqlTextBuilder.Append(sqlText);
            return this;
        }

        public MutableClause Prepend(string text)
        {
            _sqlTextBuilder.Insert(0, text);
            return this;
        }

        public MutableClause Line()
        {
            _sqlTextBuilder.Append("\n");
            return this;
        }

        public MutableClause WrapParentheses()
        {
            _sqlTextBuilder.Insert(0, "(").Append(")");
            return this;
        }

        public override string ToString()
        {
            return Text;
        }

    }
}