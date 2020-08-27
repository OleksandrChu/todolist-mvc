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

        private SqliteConnection ProvideConnection()
        {
            var connectionBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "./todos.db"
            };
            return new SqliteConnection(connectionBuilder.ConnectionString);
        }

        private void SetUpDataBase()
        {
            using (var connection = ProvideConnection())
            {
                BuildSqlCommand(connection, "CREATE TABLE IF NOT EXISTS tasks(id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(50), done BOOLEAN)").ExecuteNonQuery();
            }
        }

        private SqliteCommand BuildSqlCommand(SqliteConnection connection, string query)
        {
            var command = connection.CreateCommand();
            command.Connection.Open();
            command.CommandText = query;
            return command;
        }

        internal Task Create(Task task)
        {
            using (var connection = ProvideConnection())
            {
                BuildSqlCommand(connection, $"INSERT INTO tasks(name, done) VALUES('{task.Name}', '{task.Done}')").ExecuteNonQuery();
            }
            return task;
        }

        internal void Update(int id, JsonPatchDocument patch)
        {
            using (var connection = ProvideConnection())
            {
                BuildSqlCommand(connection, $"UPDATE tasks SET done = {patch.Done} WHERE id = {id};").ExecuteNonQuery();
            }
        }

        internal List<Task> Select()
        {
            var tasks = new List<Task>();
            using (var connection = ProvideConnection())
            {
                using (var reader = BuildSqlCommand(connection, "SELECT * FROM tasks").ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2)));
                    }
                }
            }
            return tasks;
        }
    }
}