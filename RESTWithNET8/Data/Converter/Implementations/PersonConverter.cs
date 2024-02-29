﻿using RESTWithNET8.Data.Converter.Contract;
using RESTWithNET8.Data.ValueObjects;
using RESTWithNET8.Models;

namespace RESTWithNET8.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return new Person
                {
                    Id = origin.Id,
                    FirstName = origin.FirstName,
                    LastName = origin.LastName,
                    Address = origin.Address,
                    Gender = origin.Gender
                };
            }
        }

        public PersonVO Parse(Person origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return new PersonVO
                {
                    Id = origin.Id,
                    FirstName = origin.FirstName,
                    LastName = origin.LastName,
                    Address = origin.Address,
                    Gender = origin.Gender
                };
            }
        }

        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return origin.Select(item => Parse(item)).ToList();
            }
        }

        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return origin.Select(item => Parse(item)).ToList();
            }
        }
    }
}
