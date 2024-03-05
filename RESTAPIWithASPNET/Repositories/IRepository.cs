using Microsoft.EntityFrameworkCore;
using RESTWithNET8.Models;
using RESTWithNET8.Models.Base;

namespace RESTWithNET8.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T entity);

        T FindByID(long id);

        List<T> FindAll();

        T Update(T entity);

        void Delete(long id);

        bool Exists(long id);

        List<T> FindWithPagedSearch(string query);

        int GetCount(string query);
    }
}
