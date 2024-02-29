using RESTWithNET8.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTWithNET8.Data.ValueObjects
{
    public class PersonVO
    {
        public long Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Gender {  get; set; } = string.Empty;
    }
}
