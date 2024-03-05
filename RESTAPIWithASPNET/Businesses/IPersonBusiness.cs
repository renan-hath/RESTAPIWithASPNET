using RESTWithNET8.Data.ValueObjects;
using RESTWithNET8.Models;

namespace RESTWithNET8.Businesses
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindByID(long id);

        List<PersonVO> FindByName(string name, string lastName);

        List<PersonVO> FindAll();

        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);

        PersonVO Update(PersonVO person);

        PersonVO Disable(long id);

        void Delete(long id);
    }
}
