using Microsoft.AspNetCore.Http;

namespace ThreeTierArch.Repositories.Interfaces
{
    public interface IUtilityRepo
    {
        Task SaveImage(string containerName, IFormFile file);
        Task EditImage(string containerName, IFormFile file, string dbpath);
        Task DeleteImage(string containerName,  string dbpath);
    }
}
