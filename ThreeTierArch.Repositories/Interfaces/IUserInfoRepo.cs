
using ThreeTierArch.Entities;

namespace ThreeTierArch.Repositories.Interfaces
{
    public interface IUserInfoRepo
    {
        Task RegisterUser(UserInfo userInfo);
        Task<UserInfo> GetUserInfo(string username, string password);
    }
}
