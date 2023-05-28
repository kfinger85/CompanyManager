using System.ComponentModel.DataAnnotations.Schema;
using CompanyManager.Models;

namespace CompanyManager.Models
{
    public class CompanyQualification
    {
        public long CompanyId { get; set; }
        public Company Company { get; set; }

        public long QualificationId { get; set; }
        public Qualification Qualification { get; set; }
    }
}
