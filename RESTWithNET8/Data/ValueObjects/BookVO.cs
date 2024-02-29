namespace RESTWithNET8.Data.ValueObjects
{
    public class BookVO
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public double Price { get; set; }

        public DateTime LaunchDate { get; set; }
    }
}
