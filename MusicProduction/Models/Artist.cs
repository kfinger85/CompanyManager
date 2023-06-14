namespace MusicProduction.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public ICollection<StageArtist> StageArtist { get; set; }
    }
}