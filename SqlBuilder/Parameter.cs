using System.Data;

namespace BenzeneSoft.SqlBuilder
{
    public class Parameter
    {
        private static long _paramId = 0;
        public string Name { get; }
        public object Value { get; }

        public Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public static Parameter CreateNew(object value)
        {
            return new Parameter($"@p{_paramId++}", value);
        }

        public IDbDataParameter CreateDbParameter(IDbCommand command)
        {
            var param = command.CreateParameter();
            param.ParameterName = Name;
            param.Value = Value;
            return param;
        }
    }
}