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
                connection.Execute($"INSERT INTO lists(name) VALUES(@Name)", model);
                model.Id = Convert.ToInt32(connection.Query<long>("SELECT MAX(id) FROM lists").First());
            }
            return model;
        }

        public List<TaskList> SelectAll()
        {
            using (var connection = databaseService.ProvideConnection())
            {
                Dictionary<int, TaskList> lists = new Dictionary<int, TaskList>();
                return connection.Query<TaskList, Task, TaskList>($"SELECT * FROM lists LEFT JOIN tasks ON tasks.listId = lists.id", 
                (taskList, task) => 
                {
                    TaskList currentTaskList;
                    if(lists.ContainsKey(taskList.Id))
                    {
                        currentTaskList = lists[taskList.Id];
                    } else {
                        currentTaskList = taskList;
                        currentTaskList.Tasks = new List<Task>();
                        lists.Add(currentTaskList.Id, currentTaskList);
                    }
                    currentTaskList.Tasks.Add(task);
                    return currentTaskList;
                }).Distinct().AsList();
            }
        }

        public TaskList Update(TaskList model)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                connection.Execute($"UPDATE lists SET name = @Name WHERE id = @Id;", model);
                return connection.Query<TaskList>("SELECT * FROM lists WHERE id = @Id", model).First();
            }
        }

        public void Delete(int id)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                connection.Execute($"DELETE FROM lists WHERE lists.id = @ListId", new {ListId = id});
            }
        }

       public TaskList Select(int id)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                TaskList currentTaskList = null;
                return connection.Query<TaskList, Task, TaskList>($"SELECT * FROM lists LEFT JOIN tasks ON tasks.listId = @ListId WHERE lists.id = @ListId", 
                (taskList, task) => 
                {
                    if(currentTaskList == null) {
                        currentTaskList = taskList;
                        currentTaskList.Tasks = new List<Task>();
                    }
                    currentTaskList.Tasks.Add(task);
                    return currentTaskList;
                }, new {ListId = id}).FirstOrDefault();
            }
        }
    }
}