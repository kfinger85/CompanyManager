using System.ComponentModel.DataAnnotations.Schema;
using CompanyManager.Models;

namespace CompanyManager.Models
{
    public class CompanyQualification
    {
         [ForeignKey("Company")]
        public long CompanyId { get; set; }
        public Company Company { get; set; }

        [ForeignKey("Qualification")]
        public long QualificationId { get; set; }
        public Qualification Qualification { get; set; }
    }
}
