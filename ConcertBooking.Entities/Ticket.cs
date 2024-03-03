

namespace ConcertBooking.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int SeatNumber { get; set; } 
        public bool IsBooked { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

    }
}
