using System.ComponentModel.DataAnnotations.Schema;
namespace CompanyManager.Models
{
    public class Project
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ProjectSize Size { get; set;}
        public ProjectStatus Status { get; set; }
        [NotMapped]
        public ICollection<Worker> Workers { get; set; } = new HashSet<Worker>();
        public ICollection<Qualification> Qualifications { get; set; } = new HashSet<Qualification>();
        public long CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<WorkerProject> WorkerProjects { get; set; } = new List<WorkerProject>();
        public ICollection<MissingQualification> MissingQualifications { get; set; } = new List<MissingQualification>();


        public Project()
        {
        }        
        public Project(string name, ICollection<Qualification> qualifications, ProjectSize size, Company company)
        {
            Name = name;
            Qualifications = qualifications;
            Size = size;
            Company = company;
            foreach (var qualification in qualifications)
            {
                MissingQualifications.Add(new MissingQualification(this, qualification ,qualification.Name));
            }
            Status = ProjectStatus.PLANNED;
        }

    }
}
