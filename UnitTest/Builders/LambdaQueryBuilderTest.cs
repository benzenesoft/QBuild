
using BenzeneSoft.QBuild.Builders;
using BenzeneSoft.QBuild.Functions;
using BenzeneSoft.QBuild.NameResolvers;
using NUnit.Framework;
using System.Linq;
using UnitTest.Doubles;

namespace UnitTest.Builders
{
    [TestFixture]
    public class LambdaQueryBuilderTest
    {
        private LambdaQueryBuilder _builder;
        private TestConnection _connection;

        [SetUp]
        public void Setup()
        {
            _builder = new LambdaQueryBuilder(new LowerSnakeCaseNameResolver());

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
        public void SimpleWhere()
        {
            var clause = _builder.SelectAll()
                .From<Product>()
                .Where<Product>(product => product.Id == 1)
                .Build();

            var reader = _connection.Read(clause);

            Assert.IsTrue(reader.Read());
            Assert.AreEqual(1, reader["id"]);
            Assert.AreEqual("almira", reader["name"]);
            Assert.AreEqual(80, reader["price"]);
            Assert.IsFalse(reader.Read());
        }

        [Test(Description = "select name, avg(price) from product group by name")]
        public void GroupBy()
        {
            //var reader = _connection.Read("select name, avg(price) as avg_price from product group by name");

            var clause = _builder
                .Select<Product>(product => product.Name, product => "avg(price) as avg_price")
                .From<Product>()
                .GroupBy<Product>(product => product.Name)
                .Build();

            var reader = _connection.Read(clause);

            Assert.IsTrue(reader.Read());
            Assert.AreEqual("almira", reader["name"]);
            Assert.AreEqual(75, reader["avg_price"]);
        }

        [Test(Description = "select name, avg(price) from product group by name having price > 74")]
        public void Having()
        {
            var clause = _builder
                .Select<Product>(product => product.Name, product => "avg(price) as avg_price")
                .From<Product>()
                .GroupBy<Product>(product => product.Name)
                .Having<Product>(product => FunctionFactory.Avg(product.Price) > 74)
                .Build();

            var reader = _connection.Read(clause);
            Assert.IsTrue(reader.Read());
            Assert.AreEqual("almira", reader["name"]);
            Assert.AreEqual(75, reader["avg_price"]);

            Assert.IsFalse(reader.Read());
        }

        [Test(Description = "select * order by name desc")]
        public void OrderBy()
        {
            var clause = _builder
                .Select<Product>(product => product.Name)
                .From<Product>()
                .OrderByDesc<Product>(product => product.Name)
                .Build();

            var reader = _connection.Read(clause);
            Assert.IsTrue(reader.Read());
            Assert.AreEqual("table", reader["name"]);
        }
    }
}
