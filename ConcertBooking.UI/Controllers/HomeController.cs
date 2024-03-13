using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;
using ConcertBooking.UI.Models;
using ConcertBooking.UI.ViewModels.HomePageViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ConcertBooking.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConcertRepo _concertRepo;
        private readonly ITicketRepo _ticketRepo;
        private readonly IBookingRepo _bookingRepo;
        public HomeController(ILogger<HomeController> logger, IConcertRepo concertRepo, ITicketRepo ticketRepo, IBookingRepo bookingRepo)
        {
            _logger = logger;
            _concertRepo = concertRepo;
            _ticketRepo = ticketRepo;
            _bookingRepo = bookingRepo;
        }

        public async Task<IActionResult> Index()
        {
            var concerts = await _concertRepo.GetAll();
            var homePgaeVm = concerts.Select(m => new HomeConcertVM
            {
                ConcertId = m.Id,
                Name = m.Name,
                ArtistName = m.Artist.Name,
                ConcertImageUrl = m.ImageUrl,
                Description = m.Description.Length> 35? m.Description.Substring(0, 35) : m.Description
            }).ToList();
            return View(homePgaeVm);
        }

        public async Task<IActionResult> Details(int concertId)
        {
            
            var concertDetail = await _concertRepo.GetById(concertId);
            if(concertDetail == null)
            {
                return NotFound();
            }
            else
            {
                HomePageDetailVM vm = new HomePageDetailVM();
                vm.ConcertId = concertId;
                vm.ConcertName = concertDetail.Name;
                vm.Description = concertDetail.Description;
                vm.ConcertDate = concertDetail.ConcertDate;
                vm.ConcertImageUrl = concertDetail.ImageUrl;
                vm.ArtistName = concertDetail.Artist.Name;
                vm.ArtistImageUrl = concertDetail.Artist.ImageUrl;
                vm.VenueAddress = concertDetail.Venue.Address;
                vm.VenueName = concertDetail.Venue.Name;
                return View(vm);
            }
            
        }
        [Authorize]
        public async Task<IActionResult> AvailableTickets(int concertId)
        {
            var concertDetail = await _concertRepo.GetById(concertId);
            if(concertDetail == null)
            {
                return NotFound();
            }
            else
            {
                AvailableTicketVM vm = new AvailableTicketVM();
                vm.ConcertId = concertId;
                vm.ConcertName = concertDetail.Name;
                var totalSeats = Enumerable.Range(1, concertDetail.Venue.SeatCapacity).ToList();
                var bookedSeats = await _ticketRepo.GetBookedSeatNumber(concertId);
                var availableSeats = totalSeats.Except(bookedSeats).ToList();
                vm.AvailableSeats = availableSeats;
                return View(vm);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> BookTickets(int ConcertId, List<int> selectedSeats)
        {
            if(selectedSeats == null || selectedSeats.Count == 0) {
                ModelState.AddModelError("", "No seat is selected");
                return RedirectToAction("AvailableTickets", new { concertId = ConcertId });
            }
            var newBooking = new Booking();
            newBooking.BookingDate = DateTime.Now;
            newBooking.ConcertId = ConcertId;

            //Getting UserId from Claims
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if( claimsIdentity != null)
            {
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                newBooking.UserId = userId.Value;

                foreach(var seatNumber in selectedSeats)
                {
                    newBooking.Tickets.Add(new Ticket { IsBooked = true, SeatNumber = seatNumber });
                }
                await _bookingRepo.AddBooking(newBooking);
            }
            return RedirectToAction("AvailableTickets", new { concertId = ConcertId});
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
