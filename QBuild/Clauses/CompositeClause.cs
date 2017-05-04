using System.Collections.Generic;
using System.Linq;

namespace BenzeneSoft.QBuild.Clauses
{
    public class CompositeClause : IClause
    {
        public string Text => _mutableClause.Text;
        public IEnumerable<Parameter> Parameters => _mutableClause.Parameters;

        private readonly IClause _separator;
        private readonly MutableClause _mutableClause;
        private bool _isEmpty;

        public CompositeClause(IClause separator)
        {
            _separator = separator;
            _mutableClause = new MutableClause();
            _isEmpty = true;
        }

        public CompositeClause Add(string component)
        {
            return Add(new MutableClause(component));
        }

        public CompositeClause Add(IClause component)
        {
            if (!_isEmpty)
                _mutableClause.Append(_separator);

            _isEmpty = false;
            _mutableClause.Append(component);
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