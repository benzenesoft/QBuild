using System.Data;
using System.Data.SQLite;
using System.IO;
using BenzeneSoft.SqlBuilder;
using BenzeneSoft.SqlBuilder.Builders;
using BenzeneSoft.SqlBuilder.Predicates;
using NUnit.Framework;
using UnitTest.Doubles;
using UnitTest.Entities;

namespace UnitTest
{
    [TestFixture]
    public class QueryBuilderTest
    {
        private QueryBuilder _builder;
        private IDbConnection _connection;
        private SelectBuilder<Product> _selectBuilder;
        private FromBuilder<Product> _fromBuilder;
        private WhereBuilder _whereBuilder;
        private PredicateFactory<Product> _predicateFactory;

        [SetUp]
        public void Setup()
        {
            var nameResolver = new LowerSnakeCaseNameResolver();
            _selectBuilder = new SelectBuilder<Product>(nameResolver);
            _fromBuilder = new FromBuilder<Product>(nameResolver);
            _whereBuilder = new WhereBuilder();
            _predicateFactory = new PredicateFactory<Product>(nameResolver);
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
            var sql = _builder.Select(_selectBuilder.All().Build())
                .From(_fromBuilder.Default().Build())
                .Where(_whereBuilder.Begin(_predicateFactory.Binary(p => p.Id, "=", 1)).Build())
                .Build();

            using (var command = sql.CreateDbCommand(_connection))
            {
                var reader = command.ExecuteReader();

                Assert.IsTrue(reader.Read());
                Assert.AreEqual(1, reader["id"]);
                Assert.AreEqual("almira", reader["name"]);
                Assert.AreEqual(1000, reader["price"]);
            }
        }
    }
}
