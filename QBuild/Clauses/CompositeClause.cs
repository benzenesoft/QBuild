using System.Collections.Generic;
using System.Linq;

namespace BenzeneSoft.QBuild.Clauses
{
    public class CompositeClause : IClause
    {
        public string SqlText => _clause.SqlText;
        public IEnumerable<Parameter> Parameters => _clause.Parameters;

        private readonly IClause _separator;
        private readonly Clause _clause;
        private bool _isEmpty;

        public CompositeClause(IClause separator)
        {
            _separator = separator;
            _clause = new Clause();
            _isEmpty = true;
        }

        public CompositeClause Add(string component)
        {
            return Add(new Clause(component));
        }

        public CompositeClause Add(IClause component)
        {
            if (!_isEmpty)
                _clause.Append(_separator);

            _isEmpty = false;
            _clause.Append(component);
            return this;
        }

        //TODO : optimize
        public CompositeClause AddRange(params IClause[] components)
        {
            foreach (var component in components)
            {
                Add(component);
            }

            return this;
        }

        public CompositeClause AddRange(IEnumerable<IClause> components)
        {
            return AddRange(components.ToArray());
        }
    }
}