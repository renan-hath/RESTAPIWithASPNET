using RESTWithNET8.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTWithNET8.Models
{
    [Table("books")]
    public class Book : BaseEntity
    {
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
