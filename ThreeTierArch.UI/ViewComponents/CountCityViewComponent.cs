using Microsoft.AspNetCore.Mvc;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.UI.ViewComponents
{
    public class CountCityViewComponent : ViewComponent
    {
        private readonly ICityRepo _cityRepo;

        public CountCityViewComponent(ICityRepo cityRepo)
        {
            _cityRepo = cityRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cityCount = (await _cityRepo.GetAll()).Count();
            return View(cityCount);
        }
    }
}
