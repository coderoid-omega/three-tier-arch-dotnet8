namespace ConcertBooking.UI.ViewModels
{
    public class EditConcertVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        public DateTime ConcertDate { get; set; }
        public int VenueId { get; set; }
        public int ArtistId { get; set; }
    }
}
