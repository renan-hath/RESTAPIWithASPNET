using RESTWithNET8.Models;

namespace RESTWithNET8.Businesses
{
    public interface IBookBusiness
    {
        Book Create(Book book);

        Book FindByID(long id);

        List<Book> FindAll();

        Book Update(Book book);

        void Delete(long id);
    }
}
