using BenzeneSoft.QBuild.Sqls;
using NUnit.Framework;

namespace UnitTest.Sqls
{
    [TestFixture]
    public class CompositSqlTest
    {
        [Test]
        public void Add_Single_NoSeparator()
        {
            var sql = new CompositeSql(new Sql(","));
            sql.Add("one");

            Assert.AreEqual("one", sql.SqlText);
        }

        [Test]
        public void Add_Multiple_AddsSeparator()
        {
            var sql = new CompositeSql(new Sql(","));
            sql.Add("one").Add("two");

            Assert.AreEqual("one,two", sql.SqlText);
        }
    }
}
