namespace CompanyManager.Models
{
    public class Stage
    {
        public int StageId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public ICollection<StageBand> StageBands { get; set; }
    }
}