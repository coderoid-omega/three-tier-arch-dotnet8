using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.Repositories.Implementations
{
    public class UserInfoRepo : IUserInfoRepo
    {
        private readonly ApplicationDbContext _context;

        public UserInfoRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserInfo> GetUserInfo(string username, string password)
        {
            var userInfo = await _context.UserInfos.Where(m => m.Username == username && m.Password == password).FirstOrDefaultAsync();
            return userInfo;
        }

        public async Task RegisterUser(UserInfo userInfo)
        {
            if(!await Exists(userInfo))
            {
                _context.Add(userInfo);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<bool> Exists(UserInfo userInfo)
        {
            var data  = await _context.UserInfos.AnyAsync(m => m.Username == userInfo.Username);
            return data;
        }
    }
}
