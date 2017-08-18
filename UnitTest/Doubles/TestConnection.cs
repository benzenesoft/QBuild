using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using BenzeneSoft.QBuild.Clauses;
using BenzeneSoft.QBuild.Utils;
using NUnit.Framework;

namespace UnitTest.Doubles
{
    public class TestConnection : IDbConnection
    {
        private SQLiteConnection _connection;

        public TestConnection()
        {
            _connection = new SQLiteConnection("Data Source=:memory:");
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IDbTransaction BeginTransaction()
        {
            return _connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return _connection.BeginTransaction(il);
        }

        public void Close()
        {
            _connection.Close();
        }

        public void ChangeDatabase(string databaseName)
        {
            _connection.ChangeDatabase(databaseName);
        }

        IDbCommand IDbConnection.CreateCommand()
        {
            return _connection.CreateCommand();
        }

        public SQLiteCommand CreateCommand()
        {
            return _connection.CreateCommand();
        }

        public void Open()
        {
            _connection.Open();
            using (var createTableCommand = _connection.CreateCommand())
            {
                var path = Path.Combine(TestContext.CurrentContext.TestDirectory, @"sql_files\create_table_product.sql");
                createTableCommand.CommandText = File.ReadAllText(path);
                createTableCommand.ExecuteNonQuery();
            }
        }

        public string ConnectionString
        {
            get { return _connection.ConnectionString; }
            set { _connection.ConnectionString = value; }
        }

        public int ConnectionTimeout => _connection.ConnectionTimeout;

        public string Database => _connection.Database;

        public ConnectionState State => _connection.State;

        public IDataReader Read(string query)
        {
            return Read(new MutableClause(query));
        }

        public IDataReader Read(IClause clause)
        {
            var command = clause.ToDbCommand(this);
            Console.WriteLine(clause.Text);
            return command.ExecuteReader();
        }
    }
}
