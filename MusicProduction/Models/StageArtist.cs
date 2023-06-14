namespace MusicProduction.Models
{
    public class StageArtist
    {
        public int StageArtistId { get; set; }
        public int StageId { get; set; }
        public Stage Stage { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}