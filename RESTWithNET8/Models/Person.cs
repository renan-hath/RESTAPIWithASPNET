using System.ComponentModel.DataAnnotations.Schema;

namespace RESTWithNET8.Models
{
    [Table("person")]
    public class Person
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Column("address")]
        public string Address { get; set; } = string.Empty;

        [Column("gender")]
        public string Gender {  get; set; } = string.Empty;
    }
}
