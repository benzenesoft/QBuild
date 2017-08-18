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
    public class NullityExpressionParserTest
    {
        private NullityExpressionParser _parser;
        private TestConnection _connection;

        [SetUp]
        public void Setup()
        {
            _parser = new NullityExpressionParser(new ParserLookup(new LowerSnakeCaseNameResolver(), new SqlFunctionNameResolver()));
            _connection = new TestConnection();
            _connection.Open();
        }

        [Test]
        public void CanParse_RightSideNull_GivesTrue()
        {
            Expression<Predicate<Product>> exp = product => product.Comment == null;
            var can = _parser.CanParse(exp.Body);
            IsTrue(can);
        }

        [Test]
        public void CanParse_LeftSideNull_GivesTrue()
        {
            Expression<Predicate<Product>> exp = product => null == product.Comment;
            var can = _parser.CanParse(exp.Body);
            IsTrue(can);
        }

        [Test]
        public void CanParse_BothSideNull_GivesFalse()
        {
            Expression<Predicate<Product>> exp = product => null == null;
            var can = _parser.CanParse(exp.Body);
            IsFalse(can);
        }

        [Test]
        public void CanParse_NoSideNull_GivesFalse()
        {
            Expression<Predicate<Product>> exp = product => product.Comment == "a comment";
            var can = _parser.CanParse(exp.Body);
            IsFalse(can);
        }

        [Test]
        public void IsNull_RightSideNull()
        {
            Expression<Predicate<Product>> exp = product => product.Comment == null;
            var predicate = _parser.Parse(exp.Body, ClauseContext.Where);
            var clause = new MutableClause("select * from product where ").Append(predicate).AppendText(" and name = 'bed'");

            var reader = _connection.Read(clause);
            IsTrue(reader.Read());
            AreEqual("bed", reader["name"]);
        }

        [Test]
        public void IsNull_LeftSideNull()
        {
            Expression<Predicate<Product>> exp = product => null == product.Comment ;
            var predicate = _parser.Parse(exp.Body, ClauseContext.Where);
            var clause = new MutableClause("select * from product where ").Append(predicate).AppendText(" and name = 'bed'");

            var reader = _connection.Read(clause);
            IsTrue(reader.Read());
            AreEqual("bed", reader["name"]);
        }

        [Test]
        public void IsNotNull_RightSideNull()
        {
            Expression<Predicate<Product>> exp = product => product.Comment != null;
            var predicate = _parser.Parse(exp.Body, ClauseContext.Where);
            var clause = new MutableClause("select * from product where ").Append(predicate);

            var reader = _connection.Read(clause);
            IsTrue(reader.Read());
            AreEqual("chair", reader["name"]);
        }

        [Test]
        public void IsNotNull_LeftSideNull()
        {
            Expression<Predicate<Product>> exp = product => null != product.Comment;
            var predicate = _parser.Parse(exp.Body, ClauseContext.Where);
            var clause = new MutableClause("select * from product where ").Append(predicate);

            var reader = _connection.Read(clause);
            IsTrue(reader.Read());
            AreEqual("chair", reader["name"]);
        }
    }
}
