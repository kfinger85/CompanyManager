using System.ComponentModel.DataAnnotations.Schema;
namespace CompanyManager.Models
{
    public class MissingQualification
    {
        public long ProjectId { get; set; }

        public long QualificationId { get; set; }

        public virtual String Name { get; set; }

        public virtual Project Project { get; set; }
        public virtual Qualification Qualification { get; set; }
        public MissingQualification() { }
        public MissingQualification(Project project, Qualification qualification, String name)
        {
            Project = project;
            Qualification = qualification;
            Name = name;
        }
        public static MissingQualification QualificationToMissingQualification(Project project, Qualification qualification)
        {
            return new MissingQualification(project, qualification, qualification.Name); // assuming Qualification has a property 'Name'
        }
    }
}