using Microsoft.AspNetCore.Identity;
using RESTWithNET8.Data.ValueObjects;
using RESTWithNET8.Models;

namespace RESTWithNET8.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
    }
}
