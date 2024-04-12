using RESTWithNET8.Data.ValueObjects;
using RESTWithNET8.Models;

namespace RESTWithNET8.Businesses
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);

        BookVO FindByID(long id);

        List<BookVO> FindAll();

        PagedSearchVO<BookVO> FindWithPagedSearch(string title, string sortDirection, int pageSize, int page);

        BookVO Update(BookVO book);

        void Delete(long id);
    }
}
