using System.Linq;
using BenzeneSoft.QBuild.Clauses;
using NUnit.Framework;

namespace UnitTest.Clauses
{
    [TestFixture]
    public class MutableClauseTest
    {

        [Test]
        public void Construct_Empty_TextAndParamsEmpty()
        {
            var clause = new MutableClause();
            Assert.IsEmpty(clause.Text);
            Assert.IsEmpty(clause.Parameters);
        }

        [Test]
        public void Construct_WithTextNoParam_TextMathcasAndParamsEmpty()
        {
            var text = "some clause";
            var clause = new MutableClause(text);
            Assert.AreEqual(text, clause.Text);
            Assert.IsEmpty(clause.Parameters);
        }

        [Test]
        public void Construct_WithTextAndParam_TextMathcasAndParamsMatches()
        {
            var parameter = Parameter.CreateNew("parameterized");
            var text = $"some clause {parameter.Name}";

            var clause = new MutableClause(text, parameter);

            Assert.AreEqual(text, clause.Text);
            Assert.Contains(parameter, clause.Parameters.ToList());
        }

        [Test]
        public void Append_NoWrap()
        {
            var clause = new MutableClause("some clause ");
            var clauseToAppend = new Clause("append this");
            clause.Append(clauseToAppend);

            Assert.AreEqual("some clause append this", clause.Text);
        }

        [Test]
        public void AppendLine_EndWithLine()
        {
            var clause = new MutableClause("some clause");
            clause.Line();
            
            Assert.AreEqual("some clause\n", clause.Text);
        }

        [Test]
        public void AppendText_TextAppended()
        {
            var clause = new MutableClause("some clause");
            clause.Append(" append this");

            Assert.AreEqual("some clause append this", clause.Text);
        }
    }
}
