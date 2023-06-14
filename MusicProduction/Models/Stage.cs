namespace MusicProduction.Models
{
    public class Stage
    {
        public int StageId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public ICollection<StageArtist> StageBands { get; set; }
    }
}