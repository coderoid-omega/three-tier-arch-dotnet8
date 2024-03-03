using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;
using ConcertBooking.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConcertBooking.UI.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistRepo _artistRepo;
        private readonly IUtilityRepo _utilityRepo;
        private readonly string containerName = "Artists";
        public ArtistController(IArtistRepo artistRepo, IUtilityRepo utilityRepo)
        {
            _artistRepo = artistRepo;
            _utilityRepo = utilityRepo;
        }
        public async Task<IActionResult> Index()
        {
            var artists = await _artistRepo.GetAll();
            List<ArtistViewModel> artistVm = new List<ArtistViewModel>();
            foreach (var artist in artists)
            {
                artistVm.Add(new ArtistViewModel
                {
                    Name = artist.Name,
                    Bio = artist.Bio,
                    Id = artist.Id,
                    ImageUrl = artist.ImageUrl
                });
            }
            return View(artistVm);
        }
        public async Task<IActionResult> Create()
        {
            CreateArtistViewModel createArtistViewModel = new CreateArtistViewModel();
            return View(createArtistViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArtistViewModel artist)
        {
            var _artist = new Artist { Name = artist.Name, Bio = artist.Bio };
            if (artist.ImageFile != null)
            {
                _artist.ImageUrl = await _utilityRepo.SaveImage(containerName, artist.ImageFile);
            }
            await _artistRepo.Save(_artist);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var artist = await _artistRepo.GetById(id);

            if (artist == null)
            {
                return new NotFoundResult();
            }
            else
            {
                EditArtistViewModel vm = new EditArtistViewModel
                {  
                    Id = id, Name = artist.Name, Bio = artist.Bio, ImageUrl = artist.ImageUrl 
                };
                return View(vm);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditArtistViewModel vm)
        {
            var artist = await _artistRepo.GetById(vm.Id);
            artist.Name = vm.Name;
            artist.Bio = vm.Bio;
            if(vm.ImageFile != null)
            {
                artist.ImageUrl = await _utilityRepo.EditImage(containerName, vm.ImageFile, artist.ImageUrl);
            }
            await _artistRepo.Edit(artist);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var artist = await _artistRepo.GetById(id);
            if (artist != null)
            {
                await _utilityRepo.DeleteImage(containerName, artist.ImageUrl);
                await _artistRepo.RemoveData(artist);
            }
            return RedirectToAction("Index");
        }
    }
}
