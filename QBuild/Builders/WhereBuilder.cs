using System.Collections.Generic;
using System.Linq;

namespace BenzeneSoft.QBuild.Builders
{
    public class WhereBuilder : IWhereBuilder
    {
        private List<ISql> _predicates;
        private List<string> _operators;

        public ISql Build()
        {
            if (_predicates == null)
            {
                return new Sql();
            }

            var sql = new Sql();
            sql.Append("WHERE ").Append(_predicates.First(), true);

            for (var i = 0; i < _operators.Count; i++)
            {
                sql.Line()
                    .Append($" {_operators[i]} ")
                    .Append(_predicates[i + 1], true);
            }

            return sql;
        }

        public IWhereBuilder Or(ISql predicate)
        {
            _predicates.Add(predicate);
            _operators.Add("OR");

            return this;
        }

        public IWhereBuilder And(ISql predicate)
        {
            _predicates.Add(predicate);
            _operators.Add("AND");

            return this;
        }

        public IWhereBuilder Begin(ISql predicate)
        {
            _predicates = new List<ISql>();
            _operators = new List<string>();

            _predicates.Add(predicate);

            return this;
        }
    }
}