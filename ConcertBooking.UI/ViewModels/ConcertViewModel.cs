using ConcertBooking.Entities;

namespace ConcertBooking.UI.ViewModels
{
    public class ConcertViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ConcertDate { get; set; }
        public string VenueName { get; set; }
        public string ArtistName { get; set; }
    }
}
