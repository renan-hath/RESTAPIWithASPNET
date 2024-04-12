using System.ComponentModel.DataAnnotations.Schema;

namespace RESTWithNET8.Models.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
