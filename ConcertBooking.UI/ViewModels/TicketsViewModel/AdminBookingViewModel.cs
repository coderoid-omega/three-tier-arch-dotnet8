namespace ConcertBooking.UI.ViewModels.TicketsViewModel
{
    public class AdminBookingViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ConcertName { get;set; }
        public int BookingId { get; set; }
        public DateTime Bookingdate { get; set; }
        public string SeatNumber { get; set; }
    }
}
