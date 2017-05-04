using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenzeneSoft.QBuild.Builders;

namespace BenzeneSoft.QBuild.Clauses
{
    public class Clause : IClause
    {
        private readonly StringBuilder _sqlTextBuilder;
        private readonly List<Parameter> _parameters;

        public Clause(string sqlText, params Parameter[] parameters)
        {
            _sqlTextBuilder = new StringBuilder(sqlText);
            _parameters = new List<Parameter>(parameters);
        }

        public Clause() : this(string.Empty) { }
        public Clause(IClause clause) : this(clause.SqlText, clause.Parameters.ToArray()) { }
        public Clause(ISqlBuilder builder) : this(builder.Build()) { }

        public string SqlText => _sqlTextBuilder.ToString();

        public IEnumerable<Parameter> Parameters => _parameters;

        public Clause Append(IClause clause)
        {
            if (clause == null) return this;

            var text = clause.SqlText;

            _sqlTextBuilder.Append(text);
            _parameters.AddRange(clause.Parameters);

            return this;
        }

        public Clause Append(string sqlText)
        {
            _sqlTextBuilder.Append(sqlText);
            return this;
        }

        public Clause Line()
        {
            _sqlTextBuilder.Append("\n");
            return this;
        }

        public Clause WrapParentheses()
        {
            _sqlTextBuilder.Insert(0, "(").Append(")");
            return this;
        }

        public override string ToString()
        {
            return SqlText;
        }
    }
}