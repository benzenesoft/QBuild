namespace BenzeneSoft.SqlBuilder.Builders
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
            return new Sql("FROM ").Append(_from);
        }

        IFromBuilder IFromBuilder.From(string fromExpression)
        {
            return From(fromExpression);
        }

        public IFromBuilder<T> From(string fromExpression)
        {
            _from = fromExpression;
            return this;
        }

        public IFromBuilder<T> Default()
        {
            return From(_nameResolver.Table(typeof(T)));
        }
    }
}