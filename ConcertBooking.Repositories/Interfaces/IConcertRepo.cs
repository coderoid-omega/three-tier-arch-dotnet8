using ConcertBooking.Entities;

namespace ConcertBooking.Repositories.Interfaces
{
    public interface IConcertRepo
    {
        Task<IEnumerable<Concert>> GetAll();
        Task<Concert> GetById(int id);
        Task Save(Concert concert);
        Task Edit(Concert concert);
        Task RemoveData(Concert concert);
    }
}
