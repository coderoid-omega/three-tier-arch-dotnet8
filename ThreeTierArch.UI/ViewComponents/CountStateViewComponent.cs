using Microsoft.AspNetCore.Mvc;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.UI.ViewComponents
{
    public class CountStateViewComponent : ViewComponent
    {
        private readonly IStateRepo _stateRepo;

        public CountStateViewComponent(IStateRepo stateRepo)
        {
            _stateRepo = stateRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var stateCount = (await _stateRepo.GetAllState()).Count();
            return View(stateCount);
        }
    }
}
