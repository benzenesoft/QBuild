using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Builders;
using BenzeneSoft.QBuild.Predicates;
using NUnit.Framework;
using UnitTest.Doubles;
using UnitTest.Entities;
using static NUnit.Framework.Assert;

namespace UnitTest
{
    [TestFixture]
    public class QueryBuilderTest
    {
        private QueryBuilder _builder;
        private TestConnection _connection;
        private ColumnsBuilder<Product> _columnsBuilder;
        private TablesBuilder<Product> _tablesBuilder;
        private PredicateBuilder _predicateBuilder;
        private OrderByBuilder<Product> _orderByBuilder;
        private PredicateFactory<Product> _predicateFactory;

        [SetUp]
        public void Setup()
        {
            var nameResolver = new LowerSnakeCaseNameResolver();
            _columnsBuilder = new ColumnsBuilder<Product>(nameResolver);
            _tablesBuilder = new TablesBuilder<Product>(nameResolver);
            _predicateBuilder = new PredicateBuilder();
            _predicateFactory = new PredicateFactory<Product>(nameResolver);
            _orderByBuilder = new OrderByBuilder<Product>(nameResolver);
            _builder = new QueryBuilder();

            _connection = new TestConnection();
            _connection.Open();
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Close();
            _connection.Dispose();
        }

        [Test(Description = "select * from product where id = 1")]
        public void Test1()
        {
            var sql = _builder.Select(_columnsBuilder.All().Build())
                .From(_tablesBuilder.Build())
                .Where(_predicateBuilder.Begin(_predicateFactory.Binary(p => p.Id, "=", 1)).Build())
                .Build();

            var reader = _connection.Read(sql);

            IsTrue(reader.Read());
            AreEqual(1, reader["id"]);
            AreEqual("almira", reader["name"]);
            AreEqual(80, reader["price"]);
            IsFalse(reader.Read());
        }

        [Test(Description = "select name, avg(price) from product group by name")]
        public void GroupBy()
        {
            var sql = _builder
                .Select(new Sql("name, avg(price) as avg_price"))
                .From(new Sql("product"))
                .GroupBy(new Sql("group by name"))
                .Build();

            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("almira", reader["name"]);
            AreEqual(75, reader["avg_price"]);
        }
    }
}
