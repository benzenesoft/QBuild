using System.Linq;
using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Clauses;
using NUnit.Framework;

namespace UnitTest.Clauses
{
    [TestFixture]
    public class SqlTest
    {

        [Test]
        public void Construct_Empty_TextAndParamsEmpty()
        {
            var sql = new MutableClause();
            Assert.IsEmpty(sql.Text);
            Assert.IsEmpty(sql.Parameters);
        }

        [Test]
        public void Construct_WithTextNoParam_TextMathcasAndParamsEmpty()
        {
            var someSql = "some sql";
            var sql = new MutableClause(someSql);
            Assert.AreEqual(someSql, sql.Text);
            Assert.IsEmpty(sql.Parameters);
        }

        [Test]
        public void Construct_WithTextAndParam_TextMathcasAndParamsMatches()
        {
            var parameter = Parameter.CreateNew("parameterized");
            var someSql = $"some sql {parameter.Name}";

            var sql = new MutableClause(someSql, parameter);

            Assert.AreEqual(someSql, sql.Text);
            Assert.Contains(parameter, sql.Parameters.ToList());
        }

        [Test]
        public void Append_NoWrap()
        {
            var sql = new MutableClause("some sql ");
            var sqlToAppend = new MutableClause("append this");
            sql.Append(sqlToAppend);

            Assert.AreEqual("some sql append this", sql.Text);
        }

        [Test]
        public void AppendLine_EndWithLine()
        {
            var sql = new MutableClause("some sql");
            sql.Line();
            
            Assert.AreEqual("some sql\n", sql.Text);
        }

        [Test]
        public void AppendText_TextAppended()
        {
            var sql = new MutableClause("some sql");
            sql.Append(" append this");

            Assert.AreEqual("some sql append this", sql.Text);
        }
    }
}
