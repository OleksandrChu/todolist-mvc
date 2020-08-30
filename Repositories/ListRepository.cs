using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using mvc.Models;
using mvc.Services;

namespace mvc.Repositories
{
    public class ListRepository : IDbRepository<TaskList>
    {
        private readonly DatabaseService databaseService;

        public ListRepository(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public TaskList Create(TaskList model)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                connection.Execute($"INSERT INTO lists(name) VALUES('{model.Name}')", model);
                model.Id = Convert.ToInt32(connection.Query<long>("SELECT MAX(id) FROM lists").First());
            }
            return model;
        }

        public List<TaskList> SelectAll()
        {
            using (var connection = databaseService.ProvideConnection())
            {
                return connection.Query<TaskList>("SELECT * FROM lists").AsList();
            }
        }

        public TaskList Update(TaskList model)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                connection.Execute($"UPDATE tasks SET name = @Name WHERE id = @Id;", model);
                return connection.Query<TaskList>("SELECT * FROM tasks WHERE id = @Id", model).First();
            }
        }

        public void Delete(int id)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                connection.Execute($"DELETE FROM lists WHERE id = {id}");
            }
        }

        public TaskList Select(int id)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                return connection.Query<TaskList>($"SELECT * FROM lists WHERE id = {id}").First();
            }
        }
    }
}