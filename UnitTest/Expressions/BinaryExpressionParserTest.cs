using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Expressions;
using NUnit.Framework;
using UnitTest.Doubles;
using UnitTest.Entities;

namespace UnitTest.Expressions
{
    [TestFixture]
    public class BinaryExpressionParserTest
    {
        private BinaryExpressionParser _parser;
        private TestConnection _connection;

        [SetUp]
        public void Setup()
        {
            _parser = new BinaryExpressionParser(new LowerSnakeCaseNameResolver(), new OperatorResolver());
            _connection = new TestConnection();
            _connection.Open();
        }

        [Test(Description = "find items that has discount")]
        public void Parse_ColumnColum()
        {
            Expression<Predicate<Product>> exp = p => p.DiscountedPrice < p.Price;
            var predicate = _parser.Parse(exp.Body);
            var sql = new Sql("select * from product where ").Append(predicate);
            var reader = _connection.Read(sql);
            Assert.IsTrue(reader.Read());
            Assert.AreEqual("sofa", reader["name"]);
        }

        [Test(Description = "find cheap items")]
        public void Parse_ColumnValue()
        {
            Expression<Predicate<Product>> exp = p => p.Price <= 15;
            var predicate = _parser.Parse(exp.Body);
            var sql = new Sql("select * from product where ").Append(predicate);
            var reader = _connection.Read(sql);
            Assert.IsTrue(reader.Read());
            Assert.AreEqual("bench", reader["name"]);
        }
    }
}
