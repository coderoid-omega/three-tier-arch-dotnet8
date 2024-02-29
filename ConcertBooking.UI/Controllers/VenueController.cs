using ConcertBooking.Entities;
using ConcertBooking.Repositories;
using ConcertBooking.Repositories.Interfaces;
using ConcertBooking.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConcertBooking.UI.Controllers
{
    public class VenueController : Controller
    {
        private readonly IVenueRepo _venueRepo;
        public VenueController(IVenueRepo venueRepo)
        {
            _venueRepo = venueRepo;
        }
        public async Task<IActionResult> Index()
        {
            var venues = await _venueRepo.GetAll();
            List<VenueViewModel> venueVm = new List<VenueViewModel>();
            foreach (var venue in venues)
            {
                venueVm.Add(new VenueViewModel { Address = venue.Address, Id = venue.Id, Name = venue.Name, 
                 SeatCapacity = venue.SeatCapacity});
            }
            return View(venueVm);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VenueViewModel venue)
        {
            await _venueRepo.Save(new Venue { SeatCapacity = venue.SeatCapacity, Name = venue.Name, Address = venue.Address });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var venue = await _venueRepo.GetById(id);
            
            if (venue == null)
            {
                return new NotFoundResult();
            }
            else
            {
                VenueViewModel vm = new VenueViewModel { Address = venue.Address, Id = id, Name = venue.Name, SeatCapacity = venue.SeatCapacity };
                return View(vm);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VenueViewModel venue)
        {
            var venueM = new Venue { Address = venue.Address, Id = venue.Id, Name = venue.Name, SeatCapacity = venue.SeatCapacity };
            await _venueRepo.Edit(venueM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var venue = await _venueRepo.GetById(id);
            if(venue != null)
            {
                await _venueRepo.RemoveData(venue);
            }
            return RedirectToAction("Index");
        }
    }
}
