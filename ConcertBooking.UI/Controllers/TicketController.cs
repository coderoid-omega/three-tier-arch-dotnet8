using ConcertBooking.Repositories.Interfaces;
using ConcertBooking.UI.ViewModels.TicketsViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConcertBooking.UI.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketRepo _ticketRepo;
        public TicketController(ITicketRepo ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }
        [Authorize]
        public async Task<IActionResult> MyTickets()
        {
            var cliamsIdentity = (ClaimsIdentity)User.Identity;
            string userId = cliamsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var myBookings = await _ticketRepo.GetUserBookings(userId);
            List<BookingViewModel> bookings = new List<BookingViewModel>();
            foreach(var booking in myBookings)
            {
                bookings.Add(new BookingViewModel
                {
                    BookingId = booking.Id,
                    BookingDate = booking.BookingDate,
                    ConcertName = booking.Concert.Name,
                    Tickets = booking.Tickets.Select(m => new TicketViewModel { SeatNumber = m.SeatNumber}).ToList()
                });
            }
            return View();
        }
    }
}
