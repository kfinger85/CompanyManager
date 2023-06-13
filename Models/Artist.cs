namespace CompanyManager.Models
{
    public class Band
    {
        public int BandId { get; set; }
        public string Name { get; set; }
        public ICollection<StageBand> StageBands { get; set; }
        // Other band properties...
    }
}