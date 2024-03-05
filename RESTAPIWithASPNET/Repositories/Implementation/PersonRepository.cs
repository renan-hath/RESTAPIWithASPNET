using RESTWithNET8.Models;
using RESTWithNET8.Models.Context;

namespace RESTWithNET8.Repositories.Implementation
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context) { }

        public Person Disable(long id)
        {
            if (!_context.Persons.Any(p => p.Id.Equals(id)))
            {
                return null;
            }

            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (user != null)
            {
                user.Enabled = false;

                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return user;
        }

        public List<Person> FindByName(string name, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(name) &&
                !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(p =>
                    p.FirstName.Contains(name) &&
                    p.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(name) &&
                     string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(p =>
                    p.FirstName.Contains(name)).ToList();
            }
            else if (string.IsNullOrWhiteSpace(name) &&
                     !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(p =>
                    p.LastName.Contains(lastName)).ToList();
            }

            return null;
        }
    }
}
