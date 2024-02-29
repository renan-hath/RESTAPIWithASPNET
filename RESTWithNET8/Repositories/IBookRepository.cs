using RESTWithNET8.Models;

namespace RESTWithNET8.Repositories
{
    public interface IBookRepository
    {
        Book Create(Book book);

        Book FindByID(long id);

        List<Book> FindAll();

        Book Update(Book book);

        void Delete(long id);

        bool Exists(long id);
    }
}
