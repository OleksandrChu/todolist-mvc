using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using mvc.Controllers;
using mvc.Models;

namespace mvc.Services
{
    public class DatabaseService
    {
        public DatabaseService()
        {
            SetUpDataBase();
        }

        internal SqliteConnection ProvideConnection()
        {
            var connectionBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "./todos.db"
            };
            return new SqliteConnection(connectionBuilder.ConnectionString);
        }

        internal SqliteCommand BuildSqlCommand(SqliteConnection connection, string query)
        {
            var command = connection.CreateCommand();
            command.Connection.Open();
            command.CommandText = query;
            return command;
        }

        private void SetUpDataBase()
        {
            using (var connection = ProvideConnection())
            {
                BuildSqlCommand(connection, "CREATE TABLE IF NOT EXISTS tasks(id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(50), done BOOLEAN)").ExecuteNonQuery();
            }
        }
    }
}