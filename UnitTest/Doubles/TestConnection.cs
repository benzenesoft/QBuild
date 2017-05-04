using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Clauses;

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
                createTableCommand.CommandText = File.ReadAllText("sql_files/create_table_product.sql");
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
            return Read(new Clause(query));
        }

        public SQLiteDataReader Read(IClause clause)
        {
            var command = CreateCommand();
            command.CommandText = clause.SqlText;
            foreach (var parameter in clause.Parameters)
            {
                var param = command.CreateParameter();
                param.ParameterName = parameter.Name;
                param.Value = parameter.Value;
                command.Parameters.Add(param);
            }

            Console.WriteLine(clause.SqlText);
            return command.ExecuteReader();
        }
    }
}
