using Microsoft.AspNetCore.Mvc;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.UI.ViewComponents
{
    public class CountCountryViewComponent : ViewComponent
    {
        private readonly ICountryRepo _countryRepo;

        public CountCountryViewComponent(ICountryRepo countryRepo)
        {
            _countryRepo = countryRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var countryCount = (await _countryRepo.GetAllCountry()).Count();
            return View(countryCount);
        }
    }
}
