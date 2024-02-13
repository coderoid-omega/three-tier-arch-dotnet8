using Microsoft.AspNetCore.Mvc;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.UI.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryRepo _countryRepo;
        public CountryController(ICountryRepo countryRepo) {
            _countryRepo = countryRepo;
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                var list = (await _countryRepo.GetAllCountry()).ToList();
                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public async Task<IActionResult> Create()
        {
            return View(new Country());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Country country)
        {
            await _countryRepo.SaveCountry(country);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var country = await _countryRepo.GetCountryById(id);
            return View(country);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Country country)
        {
             await _countryRepo.UpdateCountry(country);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var country = await _countryRepo.GetCountryById(id);
            return View(country);
        }

        public async Task<IActionResult> Delete(Country country)
        {
             await _countryRepo.DeleteCountry(country);
            return RedirectToAction("Index");
        }
    }
}
