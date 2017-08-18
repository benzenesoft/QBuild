
namespace BenzeneSoft.QBuild.Clauses
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
            return CreateNew($"@p{_paramId++}", value);
        }

        public static Parameter CreateNew(string name, object value)
        {
            return new Parameter(name, value);
        }

        public override string ToString()
        {
            return $"{Name}={Value}";
        }
    }
}