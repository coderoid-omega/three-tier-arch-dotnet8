using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.Repositories.Implementations
{
    public class UtilityRepo : IUtilityRepo
    {
        //following interface will be used to get physical path of the wwwroot folder of the application
        private IWebHostEnvironment _env;
        //following interface will be used to get the base url and scheme of the application
        private IHttpContextAccessor _contextAccessor;

        public UtilityRepo(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor) 
        {  
            _env = env; 
            _contextAccessor = httpContextAccessor;
        }
        public Task DeleteImage(string containerName, string dbpath)
        {
            //check if dbPath is null
            if (string.IsNullOrEmpty(dbpath))
            {
                return Task.CompletedTask;
            }
            var fileName = Path.GetFileName(dbpath);
            var folderPath = Path.Combine(_env.WebRootPath, containerName);
            var filePath = Path.Combine(folderPath, fileName);
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.CompletedTask;
        }

        public async Task<string> EditImage(string containerName, IFormFile file, string dbpath)
        {
           //Delete the image
           await DeleteImage(containerName, dbpath);
           //Save the image
           var newpath = await SaveImage(containerName, file);
           //return the new path
           return newpath;
        }

        public async Task<string> SaveImage(string containerName, IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var filename = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(_env.WebRootPath, containerName);
            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string filePath = Path.Combine(folder, filename);
            //save image file on disk
            using(MemoryStream ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var content = ms.ToArray();
                await File.WriteAllBytesAsync(filePath, content);
            }
            //Generating path to save in db
            string basepath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";
            var completePath = Path.Combine(basepath, containerName, filename).Replace("\\", "/");
            return completePath;
        }
    }
}
