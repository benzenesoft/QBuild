using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using BenzeneSoft.SqlBuilder;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class SqlTest
    {

        [Test]
        public void Construct_Empty_TextAndParamsEmpty()
        {
            var sql = new Sql();
            Assert.IsEmpty(sql.SqlText);
            Assert.IsEmpty(sql.Parameters);
        }

        [Test]
        public void Construct_WithTextNoParam_TextMathcasAndParamsEmpty()
        {
            var someSql = "some sql";
            var sql = new Sql(someSql);
            Assert.AreEqual(someSql, sql.SqlText);
            Assert.IsEmpty(sql.Parameters);
        }

        [Test]
        public void Construct_WithTextAndParam_TextMathcasAndParamsMatches()
        {
            var parameter = Parameter.CreateNew("parameterized");
            var someSql = $"some sql {parameter.Name}";

            var sql = new Sql(someSql, parameter);

            Assert.AreEqual(someSql, sql.SqlText);
            Assert.Contains(parameter, sql.Parameters.ToList());
        }

        [Test]
        public void Append_NoWrap()
        {
            var sql = new Sql("some sql ");
            var sqlToAppend = new Sql("append this");
            sql.Append(sqlToAppend);

            Assert.AreEqual("some sql append this", sql.SqlText);
        }

        [Test]
        public void Append_WrapParantheses()
        {
            var sql = new Sql("some sql ");
            var sqlToAppend = new Sql("append this");
            sql.Append(sqlToAppend, true);
            
            Assert.AreEqual("some sql (append this)", sql.SqlText);
        }

        [Test]
        public void AppendLine_EndWithLine()
        {
            var sql = new Sql("some sql");
            sql.Line();
            
            Assert.AreEqual("some sql" + Environment.NewLine, sql.SqlText);
        }

        [Test]
        public void AppendText_TextAppended()
        {
            var sql = new Sql("some sql");
            sql.Text(" append this");

            Assert.AreEqual("some sql append this", sql.SqlText);
        }

        [Test]
        public void CreateDbCommand()
        {
            using (var connection = new SQLiteConnection("Data Source=:memory:"))
            {
                connection.Open();
                using (var createTableCommand = connection.CreateCommand())
                {
                    createTableCommand.CommandText = File.ReadAllText("sql_files/create_table_product.sql");
                    createTableCommand.ExecuteNonQuery();
                }

                var sql = new Sql().Text("select * from product order by id");
                using (var command = sql.CreateDbCommand(connection))
                {
                    var reader = command.ExecuteReader();
                    Assert.IsTrue(reader.Read());
                    Assert.AreEqual(1, reader["id"]);
                    Assert.AreEqual("almira", reader["name"]);
                    Assert.AreEqual(1000, reader["price"]);
                }
            }
        }
    }
}
