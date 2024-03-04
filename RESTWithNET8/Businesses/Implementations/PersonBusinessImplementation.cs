using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RESTWithNET8.Data.Converter.Implementations;
using RESTWithNET8.Data.ValueObjects;
using RESTWithNET8.Models;
using RESTWithNET8.Models.Context;
using RESTWithNET8.Repositories;
using System;

namespace RESTWithNET8.Businesses.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository) 
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {

            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _repository.Create(_converter.Parse(person));

            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _repository.Update(_converter.Parse(person));

            return _converter.Parse(personEntity);
        }

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);

            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
