using System.Collections.Generic;
using System.Linq;
using Dapper;
using mvc.Models;
using mvc.Services;
using mvc.Extensions;

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
                connection.Execute($"DELETE FROM tasks WHERE id = @TaskId", new {TaskId = id});
            }
        }

        public Task Select(int id)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                return connection.Query<Task>($"SELECT * FROM tasks WHERE id = @TaskId", new {TaskId = id}).First();
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
                // name = @Name, done = @Done, listid = @ListId
                connection.Execute(model.BuildReplaceQuery(condition: "Id"), model);
                return connection.Query<Task>("SELECT * FROM tasks WHERE id = @Id", model).First();
            }
        }
    }
}