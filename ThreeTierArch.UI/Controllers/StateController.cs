

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.UI.Controllers
{
    public class StateController : Controller
    {
        private readonly ICountryRepo _countryRepo;
        private readonly IStateRepo _stateRepo;

        public StateController(ICountryRepo countryRepo, IStateRepo stateRepo)
        {
            _countryRepo = countryRepo;
            _stateRepo = stateRepo;
        }

        public async Task<IActionResult> Index()
        {
            var list = (await _stateRepo.GetAllState()).ToList();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var countries = await _countryRepo.GetAllCountry();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            return View(new State());
        }

        [HttpPost]
        public async Task<IActionResult> Create(State state)
        {
            await _stateRepo.SaveState(state);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var state =  await _stateRepo.GetStateById(id);
            var countries =   await _countryRepo.GetAllCountry();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            return View(state);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(State state)
        {
             await _stateRepo.UpdateState(state);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var state =  await _stateRepo.GetStateById(id);
            return View(state);
        }

        public async Task<IActionResult> Delete(State state)
        {
            await _stateRepo.DeleteState(state);
            return RedirectToAction("Index");
        }
    }
}
