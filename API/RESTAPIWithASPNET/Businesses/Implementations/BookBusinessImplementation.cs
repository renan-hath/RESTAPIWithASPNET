using RESTWithNET8.Data.Converter.Implementations;
using RESTWithNET8.Data.ValueObjects;
using RESTWithNET8.Models;
using RESTWithNET8.Repositories;

namespace RESTWithNET8.Businesses.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {

            return _converter.Parse(_repository.FindAll());
        }

        public PagedSearchVO<BookVO> FindWithPagedSearch(string title, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) &&
                        !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"SELECT * FROM books b 
                             WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(title))
            {
                query += $" AND b.title LIKE '%{title}%'";
            }

            query += $" ORDER BY b.title {sort} LIMIT {size} OFFSET {offset}";

            string countQuery = @"SELECT COUNT(*) FROM books b 
                                  WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(title))
            {
                countQuery += $" AND b.title LIKE '%{title}%'";
            }

            var books = _repository.FindWithPagedSearch(query);

            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<BookVO>
            {
                CurrentPage = page,
                List = _converter.Parse(books),
                PageSize = size,
                SortDirection = sort,
                TotalResults = totalResults
            };
        }

        public BookVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _repository.Create(_converter.Parse(book));

            return _converter.Parse(bookEntity);
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _repository.Update(_converter.Parse(book));

            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
