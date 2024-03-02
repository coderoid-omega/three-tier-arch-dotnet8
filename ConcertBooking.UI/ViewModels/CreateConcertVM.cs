namespace ConcertBooking.UI.ViewModels
{
    public class CreateConcertVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public DateTime ConcertDate { get; set; }
        public int VenueId { get; set; }
        public int ArtistId { get; set; }

    }
}
