using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Builders;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Expressions;
using NUnit.Framework;
using UnitTest.Doubles;
using UnitTest.Entities;

namespace UnitTest
{
    [TestFixture]
    public class ColumnsBuilderTest
    {
        private TestConnection _connection;
        private ColumnsBuilder builder;

        [SetUp]
        public void Setup()
        {
            builder = new ColumnsBuilder(new LambdaParser(new ParserLookup(new LowerSnakeCaseNameResolver())));
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

            Assert.AreEqual("*", columnsSql.text.Trim());
        }

        [Test]
        public void Column_String()
        {
            var columnsSql = builder.Columns("id", "name").Build();
            var sql = new MutableClause("select ").Append(columnsSql).Append(" from product");

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
            var columnsSql = builder.Columns<Product>(p => p.Id, p => p.Name).Build();
            var sql = new MutableClause("select ").Append(columnsSql).Append(" from product");

            var reader = _connection.Read(sql);
            var count = reader.FieldCount;
            var columns = new[] { "id", "name" };

            Assert.AreEqual(2, count);
            Assert.Contains(reader.GetName(0), columns);
            Assert.Contains(reader.GetName(1), columns);
        }
    }

}
