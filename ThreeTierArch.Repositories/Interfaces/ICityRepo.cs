

using ThreeTierArch.Entities;

namespace ThreeTierArch.Repositories.Interfaces
{
    public interface ICityRepo
    {
        public Task SaveCity(City city);
        public Task UpdateCity(City city);
        public Task DeleteCity(City city);
        public Task<IEnumerable<City>> GetAll();
        public Task<City?> GetByCityId(int cityId);
    }
}
