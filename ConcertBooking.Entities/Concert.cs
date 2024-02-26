namespace ConcertBooking.Entities
{
    public class Concert
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ConcertDate { get; set; }
        public int VenueId { get; set; }
        public Venue Venue { get; set; }   
        public int ArtistId { get;set; }
        public Artist Artist { get; set; }
    }
}
