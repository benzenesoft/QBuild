namespace BenzeneSoft.SqlBuilder
{
    public class FromBuilder<T> : IFromBuilder<T>
    {
        private readonly INameResolver _nameResolver;
        private string _from;

        public FromBuilder(INameResolver nameResolver)
        {
            _nameResolver = nameResolver;
        }

        public ISql Build()
        {
            return new Sql(_from);
        }

        public IFromBuilder From(string fromExpression)
        {
            _from = fromExpression;
            return this;
        }

        public IFromBuilder<T> Default()
        {
            _from = _nameResolver.Table(typeof(T));
            return this;
        }
    }
}