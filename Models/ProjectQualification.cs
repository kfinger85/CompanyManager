using CompanyManager.Models;

namespace CompanyManager.Models
{
    public class ProjectQualification
    {
        public long ProjectId { get; set; }
        public Project Project { get; set; }

        public long QualificationId { get; set; }
        public Qualification Qualification { get; set; }
    }
}
