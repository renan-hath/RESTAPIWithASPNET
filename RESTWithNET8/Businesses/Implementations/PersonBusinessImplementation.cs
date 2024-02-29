using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RESTWithNET8.Models;
using RESTWithNET8.Models.Context;
using RESTWithNET8.Repositories;
using System;

namespace RESTWithNET8.Businesses.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;

        public PersonBusinessImplementation(IRepository<Person> repository) 
        {
            _repository = repository;
        }

        public List<Person> FindAll()
        {

            return _repository.FindAll();
        }

        public Person FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
