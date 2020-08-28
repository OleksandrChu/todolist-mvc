using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using mvc.Controllers;
using mvc.Models;

namespace mvc.Services
{
    public class DatabaseService
    {
        private const string SQL_CREATE_TABLE_TASKS = "CREATE TABLE IF NOT EXISTS tasks("
            + "id INTEGER PRIMARY KEY AUTOINCREMENT, "
            + "name VARCHAR(50) NOT NULL, "
            + "done BOOLEAN NOT NULL, "
            + "list_id INTEGEGER NOT NULL, "
            + "FOREIGN KEY (list_id) REFERENCES lists (id));";
        private const string SQL_CREATE_TABLE_LISTS = "CREATE TABLE IF NOT EXISTS lists(id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(50));";
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
                BuildSqlCommand(connection, SQL_CREATE_TABLE_LISTS + SQL_CREATE_TABLE_TASKS).ExecuteNonQuery();
            }
        }
    }
}