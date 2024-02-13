using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Interfaces;
using ThreeTierArch.UI.ViewModels.City;

namespace ThreeTierArch.UI.Controllers
{
    public class CityController : Controller
    {
        private readonly ICountryRepo _countryRepo;
        private readonly IStateRepo _stateRepo;
        private readonly ICityRepo _cityRepo;

        public CityController(ICountryRepo countryRepo, IStateRepo stateRepo, ICityRepo cityRepo)
        {
            _countryRepo = countryRepo;
            _stateRepo = stateRepo;
            _cityRepo = cityRepo;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _cityRepo.GetAll();
            var listVm = list.Select(m => new CityListVM { 
                Id = m.Id,
                Name = m.Name,
                CountryName = m.State.Country == null ? "" :m.State.Country.Name,
                StateName = m.State.Name,
            }).ToList();
            return View(listVm);
        }

        public async Task<IActionResult> Create()
        {
            var countries = await _countryRepo.GetAllCountry();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            var states = await _stateRepo.GetAllState();
            ViewBag.States = new SelectList(states, "Id", "Name");
            return View(new City());
        }

        [HttpPost]
        public async Task<IActionResult> Create(City city)
        {
            await _cityRepo.SaveCity(city);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var city = await _cityRepo.GetByCityId(id);
            var states = await _stateRepo.GetAllState();
            ViewBag.States = new SelectList(states, "Id", "Name");
            return View(city);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(City city)
        {
            await _cityRepo.UpdateCity(city);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var state = await _cityRepo.GetByCityId(id);
            return View(state);
        }

        public async Task<IActionResult> Delete(City city)
        {
            await _cityRepo.DeleteCity(city);
            return RedirectToAction("Index");
        }
    }
}
