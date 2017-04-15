using System.Data.SQLite;
using System.IO;
using BenzeneSoft.SqlBuilder;
using NUnit.Framework;
using UnitTest.Entities;

namespace UnitTest
{
    [TestFixture]
    public class QueryBuilderTest
    {
        private QueryBuilder<Product> _builder;
        private SQLiteConnection _connection;

        [SetUp]
        public void Setup()
        {
            var nameResolver = new LowerSnakeCaseNameResolver();
            var selectBuilder = new SelectBuilder<Product>(nameResolver);
            var fromBuilder = new FromBuilder<Product>(nameResolver);
            var whereBuilder = new WhereBuilder();
            var predicateFactory = new PredicateFactory<Product>(nameResolver);
            _builder = new QueryBuilder<Product>(selectBuilder, fromBuilder, whereBuilder, predicateFactory);

            _connection = new SQLiteConnection("Data Source=:memory:");
            _connection.Open();
            using (var createTableCommand = _connection.CreateCommand())
            {
                createTableCommand.CommandText = File.ReadAllText("sql_files/create_table_product.sql");
                createTableCommand.ExecuteNonQuery();
            }
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
            var sql = _builder.Select(builder => builder.All())
                .From(builder => builder.Default())
                .Where((builder, factory) => builder.Begin(factory.Binary(p => p.Id, "=", 1))).Build();

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
