using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Predicates;
using NUnit.Framework;
using UnitTest.Doubles;
using UnitTest.Entities;
using static NUnit.Framework.Assert;

namespace UnitTest.Predicates
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
            var predicate = _parser.Parse<Product>(p => p.DiscountedPrice < p.Price);
            var sql = new Sql("select * from product where ").Append(predicate);
            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("sofa", reader["name"]);
        }

        [Test(Description = "find cheap items")]
        public void Parse_ColumnValue()
        {
            var predicate = _parser.Parse<Product>(p => p.Price <= 15);
            var sql = new Sql("select * from product where ").Append(predicate);
            var reader = _connection.Read(sql);
            IsTrue(reader.Read());
            AreEqual("bench", reader["name"]);
        }
    }
}
