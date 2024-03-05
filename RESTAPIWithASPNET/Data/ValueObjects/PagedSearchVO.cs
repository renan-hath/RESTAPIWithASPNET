using RESTWithNET8.Hypermedia.Abstract;

namespace RESTWithNET8.Data.ValueObjects
{
    public class PagedSearchVO<T> where T : ISupportsHyperMedia
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalResults { get; set; }

        public string SortFields { get; set; }

        public string SortDirection { get; set; }

        public Dictionary<string, object> Filters { get; set; }

        public List<T> List { get; set; }

        public PagedSearchVO()
        {
        }

        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDirection)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirection = sortDirection;
        }

        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDirection, Dictionary<string, object> filters)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirection = sortDirection;
            Filters = filters;
        }

        public PagedSearchVO(int currentPage, string sortFields, string sortDirection)
            : this(currentPage, 10, sortFields, sortDirection)
        {
        }

        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }

        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}
