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
            var clause = new SelectClause().All();
            Assert.AreEqual("*", clause.Text.Trim());
        }

        [Test]
        public void Column_String()
        {
            var clause = new SelectClause().Column("id").Column("name");
            var query = $"select {clause.Text} from product";

            var reader = _connection.Read(query);
            var count = reader.FieldCount;
            var columns = new[] { "id", "name" };

            Assert.AreEqual(2, count);
            Assert.Contains(reader.GetName(0), columns);
            Assert.Contains(reader.GetName(1), columns);
        }
    }
}
