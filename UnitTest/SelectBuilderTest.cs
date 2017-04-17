using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Builders;
using NUnit.Framework;
using UnitTest.Doubles;
using UnitTest.Entities;

namespace UnitTest
{
    [TestFixture]
    public class SelectBuilderTest
    {
        private TestConnection _connection;
        private SelectBuilder<Product> builder;

        [SetUp]
        public void Setup()
        {
            builder = new SelectBuilder<Product>(new LowerSnakeCaseNameResolver());
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
            builder.All();
            var sql = new Sql(builder).Append(" from product");

            var reader = _connection.Read(sql);
            var count = reader.FieldCount;
            var columns = new[] { "id", "name", "price" };

            Assert.AreEqual(3, count);
            Assert.Contains(reader.GetName(0), columns);
            Assert.Contains(reader.GetName(1), columns);
            Assert.Contains(reader.GetName(2), columns);
        }

        [Test]
        public void Column_String()
        {
            builder.Columns("id", "name");
            var sql = new Sql(builder).Append(" from product");

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
            builder.Columns(p => p.Id, p => p.Name);
            var sql = new Sql(builder).Append(" from product");

            var reader = _connection.Read(sql);
            var count = reader.FieldCount;
            var columns = new[] { "id", "name" };

            Assert.AreEqual(2, count);
            Assert.Contains(reader.GetName(0), columns);
            Assert.Contains(reader.GetName(1), columns);
        }
    }

}
