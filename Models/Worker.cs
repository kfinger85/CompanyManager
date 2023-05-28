using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyManager.Models.DTO;
using System.Linq;

namespace CompanyManager.Models
{
    [Table(name: "workers")]
    public class Worker
    {
        public static readonly int MAX_WORKLOAD = 12;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Salary { get; set; }

        [Required]
        [Column(name: "username", TypeName = "nvarchar(450)")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();

        public virtual ICollection<WorkerProject> WorkerProjects { get; set; } = new HashSet<WorkerProject>();

        public virtual ICollection<Qualification> Qualifications { get; set; } = new HashSet<Qualification>();

        // public ICollection<WorkerQualification> WorkerQualifications { get; set; }

        
        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }

        protected Worker() { }

        public Worker(string name, ICollection<Qualification> qualifications, double salary)
        {
            CheckArgValidity(name, qualifications, salary);
            Name = name;
            Qualifications = qualifications;
            Salary = salary;
        }

        public IEnumerable<Qualification> GetQualifications()
        {
            return Qualifications;
        }


        private void CheckArgValidity(string name, ICollection<Qualification> qualifications, double salary)
        {
            if (salary < 0)
            {
                throw new ArgumentException("Salary cannot be negative");
            }
            if (qualifications == null || qualifications.Count == 0)
            {
                throw new ArgumentException("Worker must have at least one qualification");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name field must not be null or empty");
            }
        }

        public int GetWorkload()
        {
            int workload = 0;

            foreach (var wp in WorkerProjects)
            {
                var project = wp.Project;
                if (project.Status == ProjectStatus.FINISHED) continue;
                workload += (int)project.Size; // Access the numeric value of the enum member
            }

            return workload;
        }

        public Qualification GetQualification(string name)
        {
            return Qualifications.FirstOrDefault(qual => qual.Name == name);
        }

        public bool WillOverload(Project project)
        {
            if (GetProjects().Contains(project)) return false;

            int workloadWithProject = GetWorkload() + (int)project.Size; // Access the numeric value of the enum member
            return workloadWithProject > MAX_WORKLOAD;
        }

        public bool IsAvailable()
        {
            return GetWorkload() < MAX_WORKLOAD;
        }

        public IEnumerable<Project> GetProjects()
        {
            return WorkerProjects.Select(wp => wp.Project).ToList();
        }

        public void AddProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project must not be null");
            }
            var workerProject = new WorkerProject(this, project);
            WorkerProjects.Add(workerProject);
        }

        public void RemoveProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project must not be null");
            }
            var workerProject = WorkerProjects.FirstOrDefault(wp => wp.Project == project);
            if (workerProject != null)
            {
                WorkerProjects.Remove(workerProject);
            }
        }

        public WorkerDTO ToDTO()
        {
            string[] qualificationStrings = Qualifications.Select(qual => qual.ToString()).ToArray();
            string[] projectStrings = Projects.Select(proj => proj.Name).ToArray();

            return new WorkerDTO(Name, Salary, GetWorkload(), projectStrings, qualificationStrings);
        }
    }
}
