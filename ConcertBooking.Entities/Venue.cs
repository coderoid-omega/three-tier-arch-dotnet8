

namespace ConcertBooking.Entities
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int SeatCapacity { get; set; }
        public ICollection<Concert> Concerts { get; set; } = new List<Concert>();
    }
}
