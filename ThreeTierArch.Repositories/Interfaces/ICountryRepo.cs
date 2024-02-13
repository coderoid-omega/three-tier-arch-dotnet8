
using ThreeTierArch.Entities;

namespace ThreeTierArch.Repositories.Interfaces
{
    public interface ICountryRepo
    {
        public Task SaveCountry(Country country);
        public Task UpdateCountry(Country country);
        public Task DeleteCountry(Country country);
        public Task<IEnumerable<Country>> GetAllCountry();
        public Task<Country?> GetCountryById(int id);


    }
}
