using CompanyManager.Models;

namespace CompanyManager.Models
{
    public class WorkerQualification
    {
        public long WorkerId { get; set; }
        public Worker Worker { get; set; }

        public long QualificationId { get; set; }
        public Qualification Qualification { get; set; }
    }
}
