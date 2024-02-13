using Microsoft.AspNetCore.Http;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.Repositories.Implementations
{
    public class UtilityRepo : IUtilityRepo
    {
        public Task DeleteImage(string containerName, string dbpath)
        {
            throw new NotImplementedException();
        }

        public Task EditImage(string containerName, IFormFile file, string dbpath)
        {
            throw new NotImplementedException();
        }

        public Task SaveImage(string containerName, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
