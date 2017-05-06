using BenzeneSoft.QBuild.Clauses;
using NUnit.Framework;
using UnitTest.Doubles;

namespace UnitTest.Clauses
{
    [TestFixture]
    public class SelectClauseTest
    {
        private TestConnection _connection;

        [SetUp]
        public void Setup()
        {
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
            var columnsSql = new SelectClause().All();
            Assert.AreEqual("*", columnsSql.Text.Trim());
        }

        [Test]
        public void Column_String()
        {
            var columnsSql = new SelectClause().Column("id").Column("name");
            var sql = $"select {columnsSql.Text} from product";

            var reader = _connection.Read(sql);
            var count = reader.FieldCount;
            var columns = new[] { "id", "name" };

            Assert.AreEqual(2, count);
            Assert.Contains(reader.GetName(0), columns);
            Assert.Contains(reader.GetName(1), columns);
        }
    }
}
