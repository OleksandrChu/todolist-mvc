using System.Collections.Generic;
using System.Linq;
using Dapper;
using mvc.Models;
using mvc.Services;

namespace mvc.Repositories
{
    public class TaskRepository : IDbRepository<Task>
    {
        private readonly DatabaseService databaseService;

        public TaskRepository(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public Task Create(Task model)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                connection.Execute($"INSERT INTO tasks(name, done, listId) VALUES(@Name, @Done, @ListId)", model);
                return connection.Query<Task>("SELECT * FROM tasks WHERE id = (SELECT MAX(id) FROM tasks)").First();
            }
        }

        public void Delete(int id)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                connection.Execute($"DELETE FROM tasks WHERE id = {id}");
            }
        }

        public Task Select(int id)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                return connection.Query<Task>($"SELECT * FROM tasks WHERE id = {id}").First();
            }
        }

        public List<Task> SelectAll()
        {
            using (var connection = databaseService.ProvideConnection())
            {
                return connection.Query<Task>("SELECT * FROM tasks").AsList();
            }
        }

        public Task Update(Task model)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                connection.Execute($"UPDATE tasks SET name = @Name, done = @Done, listId = @ListId WHERE id = @Id;", model);
                return connection.Query<Task>("SELECT * FROM tasks WHERE id = @Id", model).First();
            }
        }
    }
}