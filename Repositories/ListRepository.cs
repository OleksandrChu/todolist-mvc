using System;
using mvc.Models;

namespace mvc.Services
{
    public class ListRepository
    {
        private readonly DatabaseService databaseService;

        public ListRepository(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }
        internal TaskList CreateList(TaskList list)
        {
            using (var connection = databaseService.ProvideConnection())
            {
                databaseService.BuildSqlCommand(connection, $"INSERT INTO lists(name) VALUES('{list.Name}')").ExecuteNonQuery();
                long lastId = (Int64)databaseService.BuildSqlCommand(connection, $"SELECT last_insert_rowid()").ExecuteScalar();
                list.Id = Convert.ToInt32(lastId);
            }
            return list;
        }
    }
}