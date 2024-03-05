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

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) &&
                        !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"SELECT * FROM persons p 
                             WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(name))
            {
                query += $" AND p.first_name LIKE '%{name}%'";
            }

            query += $" ORDER BY p.first_name {sort} LIMIT {size} OFFSET {offset}";

            string countQuery = @"SELECT COUNT(*) FROM persons p 
                                  WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(name))
            {
                countQuery += $" AND p.first_name LIKE '%{name}%'";
            }

            var persons = _repository.FindWithPagedSearch(query);

            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirection = sort,
                TotalResults = totalResults
            };
        }

        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public List<PersonVO> FindByName(string name, string lastName)
        {
            return _converter.Parse(_repository.FindByName(name, lastName));
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
