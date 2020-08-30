using System.Collections.Generic;

namespace mvc.Repositories
{
    public interface IDbRepository<T>
    {
        T Create(T model);

        List<T> SelectAll();

        T Select(int id);

        T Update(T model);

        void Delete(int id);
    }
}