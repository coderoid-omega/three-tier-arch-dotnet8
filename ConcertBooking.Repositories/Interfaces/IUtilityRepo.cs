using Microsoft.AspNetCore.Http;

namespace ConcertBooking.Repositories.Interfaces
{
    public interface IUtilityRepo
    {
        Task<string> SaveImage(string containerName, IFormFile file);
        Task<string> EditImage(string containerName, IFormFile file, string dbpath);
        Task DeleteImage(string containerName,  string dbpath);
    }
}
