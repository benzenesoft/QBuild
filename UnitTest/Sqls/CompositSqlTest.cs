using BenzeneSoft.QBuild.Clauses;
using NUnit.Framework;

namespace UnitTest.Sqls
{
    [TestFixture]
    public class CompositSqlTest
    {
        [Test]
        public void Add_Single_NoSeparator()
        {
            var sql = new CompositeClause(new MutableClause(","));
            sql.Add("one");

            Assert.AreEqual("one", sql.text);
        }

        [Test]
        public void Add_Multiple_AddsSeparator()
        {
            var sql = new CompositeClause(new MutableClause(","));
            sql.Add("one").Add("two");

            Assert.AreEqual("one,two", sql.text);
        }
    }
}
