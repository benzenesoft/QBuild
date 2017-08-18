using System;
using System.Linq.Expressions;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Expressions;
using BenzeneSoft.QBuild.NameResolvers;
using NUnit.Framework;
using UnitTest.Doubles;
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
            _parser = new BinaryExpressionParser(new ParserLookup(new LowerSnakeCaseNameResolver(), new SqlFunctionNameResolver()));
            _connection = new TestConnection();
            _connection.Open();
        }

        [Test(Description = "find items that has discount")]
        public void Parse_ColumnAndColumnComparison()
        {
            Expression<Predicate<Product>> exp = p => p.DiscountedPrice < p.Price;
            var predicate = _parser.Parse((BinaryExpression)exp.Body, ClauseContext.Where);
            var clause = new MutableClause("select * from product where ").Append(predicate);
            var reader = _connection.Read(clause);
            IsTrue(reader.Read());
            AreEqual("sofa", reader["name"]);
        }

        [Test(Description = "find cheap items")]
        public void Parse_ColumnAndValueComparison()
        {
            Expression<Predicate<Product>> exp = p => p.Price <= 15;
            var predicate = _parser.Parse((BinaryExpression)exp.Body, ClauseContext.Where);
            var clause = new MutableClause("select * from product where ").Append(predicate);
            var reader = _connection.Read(clause);
            IsTrue(reader.Read());
            AreEqual("bench", reader["name"]);
        }

        [Test(Description = "cheap or has discount")]
        public void Parse_Or()
        {
            Expression<Predicate<Product>> exp = p => (p.Price <= 15) || (p.DiscountedPrice < p.Price);

            var eBody = exp.Body;
            var predicate = _parser.Parse(eBody, ClauseContext.Where);

            var clause = new MutableClause("select * from product where ").Append(predicate).Append(" order by price");
            var reader = _connection.Read(clause);

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
            var predicate = _parser.Parse(eBody, ClauseContext.Where);

            var clause = new MutableClause("select * from product where ").Append(predicate).Append(" order by price");
            var reader = _connection.Read(clause);

            IsTrue(_parser.CanParse(eBody));

            IsTrue(reader.Read());
            AreEqual("sofa", reader["name"]);

            IsFalse(reader.Read());
        }

        [Test(Description = "cheap or (mid and has discount)")]
        public void Parse_AndOr()
        {
            Expression<Predicate<Product>> exp = p => (p.Price <= 15) || p.DiscountedPrice < p.Price && p.Price <= 60;

            var predicate = _parser.Parse(exp.Body, ClauseContext.Where);

            var clause = new MutableClause("select count(*) as count from product where ")
                .Append(predicate);
            var reader = _connection.Read(clause);

            IsTrue(reader.Read());
            AreEqual(2, reader.GetInt32(0));

            IsFalse(reader.Read());
        }

        [Test(Description = "At least 15% discount")]
        public void ArithmeticOperations()
        {
            Expression<Predicate<Product>> exp = p => (100 * (p.Price - p.DiscountedPrice) / p.Price) + 1 >= 16;
            var predicate = _parser.Parse(exp.Body, ClauseContext.Where);

            var clause = new MutableClause("select * from product where ").Append(predicate);

            var reader = _connection.Read(clause);
            IsTrue(reader.Read());
            AreEqual("sofa", reader["name"]);
            AreEqual("l", reader["size"]);
        }

        [Test]
        public void SimpleBoolean_False()
        {
            Expression<Predicate<Product>> exp = product => product.IsAvailable == false;
            var predicate = _parser.Parse(exp.Body, ClauseContext.Where);
            var clause = new MutableClause("select * from product where ").Append(predicate).Append(" order by name");

            var reader = _connection.Read(clause);
            IsTrue(reader.Read());
            AreEqual("almira", reader["name"]);
        }


        [Test]
        public void SimpleBoolean_True()
        {
            Expression<Predicate<Product>> exp = product => product.IsAvailable == true;
            var predicate = _parser.Parse(exp.Body, ClauseContext.Where);
            var clause = new MutableClause("select * from product where ").Append(predicate).Append(" order by name");

            var reader = _connection.Read(clause);
            IsTrue(reader.Read());
            AreEqual("bed", reader["name"]);
        }
    }
}
