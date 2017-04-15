namespace BenzeneSoft.SqlBuilder
{
    public interface IFromBuilder : ISqlBuilder
    {
        IFromBuilder From(string fromExpression);
    }

    public interface IFromBuilder<T> : IFromBuilder
    {
        IFromBuilder<T> Default();
    }
}