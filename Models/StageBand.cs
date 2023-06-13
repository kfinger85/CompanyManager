namespace CompanyManager.Models
{
    public class StageBand
    {
        public int StageId { get; set; }
        public Stage Stage { get; set; }
        public int BandId { get; set; }
        public Band Band { get; set; }
    }
}