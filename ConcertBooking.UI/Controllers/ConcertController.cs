using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;
using ConcertBooking.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConcertBooking.UI.Controllers
{
    public class ConcertController : Controller
    {
        private readonly IConcertRepo _concertRepo;
        private readonly IUtilityRepo _utilityRepo;
        private readonly IArtistRepo _artistRepo;
        private readonly IVenueRepo _venueRepo;
        private readonly string containerName = "Concerts";
        public ConcertController(IConcertRepo concertRepo, IUtilityRepo utilityRepo, IArtistRepo artistRepo, IVenueRepo venueRepo)
        {
            _concertRepo = concertRepo;
            _utilityRepo = utilityRepo;
            _artistRepo = artistRepo;
            _venueRepo = venueRepo;
        }
        public async Task<IActionResult> Index()
        {
            var concerts =  await _concertRepo.GetAll();
            var vms =concerts.Select(m => new ConcertViewModel
            {
                Id = m.Id,
                ArtistName = m.Artist.Name,
                VenueName = m.Venue.Name,
                ConcertDate = m.ConcertDate,
                Name = m.Name,
            }).ToList();
            return View(vms);
        }

        public async Task<IActionResult> Create()
        {
            CreateConcertVM vm = new CreateConcertVM();
            var artists = await _artistRepo.GetAll();
            var venues = await _venueRepo.GetAll();
            ViewBag.Artists = new SelectList(artists, "Id", "Name");
            ViewBag.Venues = new SelectList(venues, "Id", "Name");
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateConcertVM vm)
        {
            var concert = new Concert
            {
                Name = vm.Name,
                Description = vm.Description,
                ConcertDate = vm.ConcertDate,
                ArtistId = vm.ArtistId,
                VenueId = vm.VenueId,
            };
            if(vm != null)
            {
                concert.ImageUrl = await _utilityRepo.SaveImage(containerName, vm.ImageFile);
            }
            await _concertRepo.Save(concert);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var concert = await _concertRepo.GetById(id);
            EditConcertVM vM = new EditConcertVM();
            vM.Name = concert.Name;
            vM.Description = concert.Description;
            vM.ConcertDate = concert.ConcertDate;
            vM.ArtistId = concert.ArtistId;
            vM.VenueId = concert.VenueId;
            vM.Id = concert.Id;
            vM.ImageUrl = concert.ImageUrl;
            var artists = await _artistRepo.GetAll();
            var venues = await _venueRepo.GetAll();
            ViewBag.Artists = new SelectList(artists, "Id", "Name");
            ViewBag.Venues = new SelectList(venues, "Id", "Name");
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditConcertVM vm)
        {
            var concert = await _concertRepo.GetById(vm.Id);
            if(concert != null)
            {
                concert.ConcertDate = vm.ConcertDate;
                concert.ArtistId = vm.ArtistId;
                concert.VenueId=vm.VenueId;
                concert.Description = vm.Description;
                concert.Name = vm.Name;

                if (vm.ImageFile != null)
                {
                    concert.ImageUrl = await _utilityRepo.EditImage(containerName, vm.ImageFile, concert.ImageUrl);
                }
                await _concertRepo.Edit(concert);
            }
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var concert = await _concertRepo.GetById(id);
            await _utilityRepo.DeleteImage(containerName, concert.ImageUrl);
            await _concertRepo.RemoveData(concert);
            return RedirectToAction("Index");
        }
    }
}
