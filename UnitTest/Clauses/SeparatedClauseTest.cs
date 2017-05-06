using BenzeneSoft.QBuild.Clauses;
using NUnit.Framework;

namespace UnitTest.Clauses
{
    [TestFixture]
    public class SeparatedClauseTest
    {
        [Test]
        public void Add_Single_NoSeparator()
        {
            var clause = new SeparatedClause(new MutableClause(","));
            clause.AppendSeparated("one");

            Assert.AreEqual("one", clause.Text);
        }

        [Test]
        public void Add_Multiple_AddsSeparator()
        {
            var clause = new SeparatedClause(new MutableClause(","));
            clause.AppendSeparated("one").AppendSeparated("two");

            Assert.AreEqual("one,two", clause.Text);
        }
    }
}
