using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using mvc.Controllers;
using mvc.Models;
using mvc.Services;

namespace mvc.Repositories
{
    public class TaskRepository
    {
        private readonly DatabaseService databaseService;

        public TaskRepository(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        internal Task Create(Task task)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                databaseService.BuildSqlCommand(connection, $"INSERT INTO tasks(name, done) VALUES('{task.Name}', '{task.Done}')").ExecuteNonQuery();
                long lastId = (Int64)databaseService.BuildSqlCommand(connection, $"SELECT last_insert_rowid()").ExecuteScalar();
                task.Id = Convert.ToInt32(lastId);
            }
            return task;
        }

        internal Task Update(int id, JsonPatchDocument patch)
        {   
            Task updatedTask = null;
            using (var connection = databaseService.ProvideConnection())
            {
                databaseService.BuildSqlCommand(connection, $"UPDATE tasks SET done = {patch.Done} WHERE id = {id};").ExecuteNonQuery();
                using(var reader = databaseService.BuildSqlCommand(connection, $"SELECT * FROM tasks WHERE id = {id}").ExecuteReader())
                {
                    if (reader.Read())
                    {
                        updatedTask = new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2));
                    }
                }
            }
            return updatedTask;
        }

        internal List<Task> SelectAll()
        {
            var tasks = new List<Task>();
            using (var connection = databaseService.ProvideConnection())
            {
                using (var reader = databaseService.BuildSqlCommand(connection, "SELECT * FROM tasks").ExecuteReader())
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