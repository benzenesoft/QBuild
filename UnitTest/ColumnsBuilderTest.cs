using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Builders;
using NUnit.Framework;
using UnitTest.Doubles;
using UnitTest.Entities;

namespace UnitTest
{
    [TestFixture]
    public class ColumnsBuilderTest
    {
        private TestConnection _connection;
        private ColumnsBuilder<Product> builder;

        [SetUp]
        public void Setup()
        {
            builder = new ColumnsBuilder<Product>(new LowerSnakeCaseNameResolver());
            _connection = new TestConnection();
            _connection.Open();
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Dispose();
        }

        [Test]
        public void All()
        {
            var columnsSql = builder.All().Build();

            Assert.AreEqual("*", columnsSql.SqlText.Trim());
        }

        [Test]
        public void Column_String()
        {
            var columnsSql = builder.Columns("id", "name").Build();
            var sql = new Sql("select ").Append(columnsSql).Append(" from product");

            var reader = _connection.Read(sql);
            var count = reader.FieldCount;
            var columns = new[] { "id", "name" };

            Assert.AreEqual(2, count);
            Assert.Contains(reader.GetName(0), columns);
            Assert.Contains(reader.GetName(1), columns);
        }

        [Test]
        public void Column_Expression()
        {
            var columnsSql = builder.Columns(p => p.Id, p => p.Name).Build();
            var sql = new Sql("select ").Append(columnsSql).Append(" from product");

            var reader = _connection.Read(sql);
            var count = reader.FieldCount;
            var columns = new[] { "id", "name" };

            Assert.AreEqual(2, count);
            Assert.Contains(reader.GetName(0), columns);
            Assert.Contains(reader.GetName(1), columns);
        }
    }

}
