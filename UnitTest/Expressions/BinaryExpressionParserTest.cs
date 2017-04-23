using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Expressions;
using NUnit.Framework;
using UnitTest.Doubles;
using UnitTest.Entities;
using static NUnit.Framework.Assert;

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
            _parser = new BinaryExpressionParser(new ParserLookup(new LowerSnakeCaseNameResolver()));
            _connection = new TestConnection();
            _connection.Open();
        }

        [Test(Description = "find items that has discount")]
        public void Parse_ColumnColum()
        {
            Expression<Predicate<Product>> exp = p => p.DiscountedPrice < p.Price;
            var predicate = _parser.Parse((BinaryExpression)exp.Body);
            var sql = new Sql("select * from product where ").Append(predicate);
            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("sofa", reader["name"]);
        }

        [Test(Description = "find cheap items")]
        public void Parse_ColumnValue()
        {
            Expression<Predicate<Product>> exp = p => p.Price <= 15;
            var predicate = _parser.Parse((BinaryExpression)exp.Body);
            var sql = new Sql("select * from product where ").Append(predicate);
            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("bench", reader["name"]);
        }

        [Test(Description = "cheap or has discount")]
        public void Parse_Or()
        {
            Expression<Predicate<Product>> exp = p => (p.Price <= 15) || (p.DiscountedPrice < p.Price);

            var eBody = exp.Body;
            var predicate = _parser.Parse(eBody);

            var sql = new Sql("select * from product where ").Append(predicate).Append(" order by price");
            var reader = _connection.Read(sql);

            IsTrue(_parser.CanParse(eBody));

            IsTrue(reader.Read());
            AreEqual("bench", reader["name"]);

            IsTrue(reader.Read());
            AreEqual("sofa", reader["name"]);

            IsTrue(reader.Read());
            AreEqual("sofa", reader["name"]);

            IsFalse(reader.Read());
        }

        [Test(Description = "expensive and has discount")]
        public void Parse_And()
        {
            Expression<Predicate<Product>> exp = p => (p.Price >= 65) && (p.DiscountedPrice < p.Price);

            var eBody = exp.Body;
            var predicate = _parser.Parse(eBody);

            var sql = new Sql("select * from product where ").Append(predicate).Append(" order by price");
            var reader = _connection.Read(sql);

            IsTrue(_parser.CanParse(eBody));

            IsTrue(reader.Read());
            AreEqual("sofa", reader["name"]);

            IsFalse(reader.Read());
        }
    }
}
