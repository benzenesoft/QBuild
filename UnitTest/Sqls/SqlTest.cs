using System.Linq;
using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Clauses;
using NUnit.Framework;

namespace UnitTest.Sqls
{
    [TestFixture]
    public class SqlTest
    {

        [Test]
        public void Construct_Empty_TextAndParamsEmpty()
        {
            var sql = new Clause();
            Assert.IsEmpty(sql.SqlText);
            Assert.IsEmpty(sql.Parameters);
        }

        [Test]
        public void Construct_WithTextNoParam_TextMathcasAndParamsEmpty()
        {
            var someSql = "some sql";
            var sql = new Clause(someSql);
            Assert.AreEqual(someSql, sql.SqlText);
            Assert.IsEmpty(sql.Parameters);
        }

        [Test]
        public void Construct_WithTextAndParam_TextMathcasAndParamsMatches()
        {
            var parameter = Parameter.CreateNew("parameterized");
            var someSql = $"some sql {parameter.Name}";

            var sql = new Clause(someSql, parameter);

            Assert.AreEqual(someSql, sql.SqlText);
            Assert.Contains(parameter, sql.Parameters.ToList());
        }

        [Test]
        public void Append_NoWrap()
        {
            var sql = new Clause("some sql ");
            var sqlToAppend = new Clause("append this");
            sql.Append(sqlToAppend);

            Assert.AreEqual("some sql append this", sql.SqlText);
        }

        [Test]
        public void AppendLine_EndWithLine()
        {
            var sql = new Clause("some sql");
            sql.Line();
            
            Assert.AreEqual("some sql\n", sql.SqlText);
        }

        [Test]
        public void AppendText_TextAppended()
        {
            var sql = new Clause("some sql");
            sql.Append(" append this");

            Assert.AreEqual("some sql append this", sql.SqlText);
        }
    }
}
