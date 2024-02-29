using System.ComponentModel.DataAnnotations.Schema;

namespace RESTWithNET8.Models
{
    [Table("books")]
    public class Book
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("author")]
        public string Author { get; set; } = string.Empty;

        [Column("price")]
        public double Price { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }
    }
}
