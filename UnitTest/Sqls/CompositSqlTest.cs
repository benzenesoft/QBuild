/*
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
            var sql = new SeparatedClause(new MutableClause(","));
            sql.AppendSeparated("one");

            Assert.AreEqual("one", sql.Text);
        }

        [Test]
        public void Add_Multiple_AddsSeparator()
        {
            var sql = new SeparatedClause(new MutableClause(","));
            sql.AppendSeparated("one").AppendSeparated("two");

            Assert.AreEqual("one,two", sql.Text);
        }
    }
}
*/
