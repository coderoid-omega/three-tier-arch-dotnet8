namespace ConcertBooking.UI.ViewModels
{
    public class EditArtistViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public IFormFile ImageFile { get; set; }
    }
}
