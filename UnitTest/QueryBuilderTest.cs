using System;
using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Builders;
using BenzeneSoft.QBuild.Clauses;
using NUnit.Framework;
using UnitTest.Doubles;
using static NUnit.Framework.Assert;

namespace UnitTest
{
    [TestFixture]
    public class QueryBuilderTest
    {
        private QueryBuilder _builder;
        private TestConnection _connection;

        [SetUp]
        public void Setup()
        {
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
            var sql = _builder.Select(new MutableClause("*"))
                .From(new MutableClause("product"))
                .Where(new MutableClause("id = 1"))
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
                .Select(new MutableClause("name, avg(price) as avg_price"))
                .From(new MutableClause("product"))
                .GroupBy(new MutableClause("name"))
                .Build();

            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("almira", reader["name"]);
            AreEqual(75, reader["avg_price"]);
        }

        [Test(Description = "select name, avg(price) from product group by name having price > 74")]
        public void Having()
        {
            var sql = _builder
                .Select(new MutableClause("name, avg(price) as avg_price"))
                .From(new MutableClause("product"))
                .GroupBy(new MutableClause("name"))
                .Having(new MutableClause("avg_price > 74"))
                .Build();

            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("almira", reader["name"]);
            AreEqual(75, reader["avg_price"]);

            IsFalse(reader.Read());
        }

        [Test(Description = "select * order by name desc")]
        public void OrderBy()
        {
            var sql = _builder
                .Select(new MutableClause("name"))
                .From(new MutableClause("product"))
                .OrderBy(new MutableClause("name desc"))
                .Build();

            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("table", reader["name"]);
        }
    }
}
