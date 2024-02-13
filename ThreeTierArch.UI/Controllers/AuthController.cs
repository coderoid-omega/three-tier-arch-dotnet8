using Microsoft.AspNetCore.Mvc;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserInfoRepo _userInfoRepo;

        public AuthController(IUserInfoRepo userInfoRepo)
        {
            _userInfoRepo = userInfoRepo;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserInfo userInfo)
        {
            await _userInfoRepo.RegisterUser(userInfo);
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserInfo userInfo)
        {
            var user = await _userInfoRepo.GetUserInfo(userInfo.Username, userInfo.Password);
            if(user == null) { return RedirectToAction("Login"); }
            else 
            {
                HttpContext.Session.SetInt32("UserId", userInfo.UserId);
                HttpContext.Session.SetString("UserName", userInfo.Username);
                return RedirectToAction("Index", "Country"); 
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
