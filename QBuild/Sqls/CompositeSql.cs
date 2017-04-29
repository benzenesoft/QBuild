using System.Collections.Generic;
using System.Linq;

namespace BenzeneSoft.QBuild.Sqls
{
    public class CompositeSql : ISql
    {
        public string SqlText => _sql.SqlText;
        public IEnumerable<Parameter> Parameters => _sql.Parameters;

        private readonly ISql _separator;
        private readonly Sql _sql;
        private bool _isEmpty;

        public CompositeSql(ISql separator)
        {
            _separator = separator;
            _sql = new Sql();
            _isEmpty = true;
        }

        public CompositeSql Add(string component)
        {
            return Add(new Sql(component));
        }

        public CompositeSql Add(ISql component)
        {
            if (!_isEmpty)
                _sql.Append(_separator);

            _isEmpty = false;
            _sql.Append(component);
            return this;
        }

        //TODO : optimize
        public CompositeSql AddRange(params ISql[] components)
        {
            foreach (var component in components)
            {
                Add(component);
            }

            return this;
        }

        public CompositeSql AddRange(IEnumerable<ISql> components)
        {
            return AddRange(components.ToArray());
        }
    }
}