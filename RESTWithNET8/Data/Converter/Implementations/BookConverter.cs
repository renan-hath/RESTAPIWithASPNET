using RESTWithNET8.Data.Converter.Contract;
using RESTWithNET8.Data.ValueObjects;
using RESTWithNET8.Models;

namespace RESTWithNET8.Data.Converter.Implementations
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return new Book
                {
                    Id = origin.Id,
                    Title = origin.Title,
                    Author = origin.Author,
                    Price = origin.Price,
                    LaunchDate = origin.LaunchDate
                };
            }
        }

        public BookVO Parse(Book origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return new BookVO
                {
                    Id = origin.Id,
                    Title = origin.Title,
                    Author = origin.Author,
                    Price = origin.Price,
                    LaunchDate = origin.LaunchDate
                };
            }
        }

        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return origin.Select(item => Parse(item)).ToList();
            }
        }

        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return origin.Select(item => Parse(item)).ToList();
            }
        }
    }
}
