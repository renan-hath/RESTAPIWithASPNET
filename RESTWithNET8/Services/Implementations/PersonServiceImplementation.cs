using RESTWithNET8.Models;

namespace RESTWithNET8.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        // Counter responsible for generating a fake ID since we are not accessing any database
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            //throw new NotImplementedException();
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();

            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }

            return persons;
        }

        public Person FindByID(long id)
        {
            return new Person
            {
                Id = 1,
                FirstName = "Leandro",
                LastName = "Costa",
                Address = "Uberlândia - MG",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = GetAndIncrement(),
                FirstName = "Person Name " + i,
                LastName = "Person LastName " + i,
                Address = "An address " + i,
                Gender = "Male"
            };
        }

        private long GetAndIncrement()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
