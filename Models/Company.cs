using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManager.Models
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Worker> Workers { get; set; } = new HashSet<Worker>();

        [NotMapped]
        public ICollection<Worker> AvailableWorkers { get; set; } = new HashSet<Worker>();

        [NotMapped]
        public ICollection<Worker> AssignedWorkers { get; set; } = new HashSet<Worker>();

        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();

        public virtual ICollection<Qualification> Qualifications { get; set; } = new HashSet<Qualification>();

        public Company()
        {
            // Default constructor required by Entity Framework
        }

        public Company(string name)
        {
            if (string.IsNullOrEmpty(name) || IsAllBlankSpace(name))
            {
                throw new ArgumentException("Name must not be null or empty");
            }
            Name = name;
            Workers = new HashSet<Worker>();
            AvailableWorkers = new HashSet<Worker>();
            AssignedWorkers = new HashSet<Worker>();
            Projects = new HashSet<Project>();
            Qualifications = new HashSet<Qualification>();
        }

        public static Company GetCompany(string name)
        {
            return new Company(name);
        }

        private bool IsAllBlankSpace(string name)
        {
            Regex pattern = new Regex("^\\s*$");
            return pattern.IsMatch(name);
        }

        public override bool Equals(object other)
        {
            if (other == null || GetType() != other.GetType())
            {
                return false;
            }
            Company company = (Company)other;
            return company.Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name}:{AvailableWorkers.Count}:{Projects.Count}";
        }

        public Worker CreateWorker(string name, ICollection<Qualification> qualifications, double salary)
        {
            Worker newWorker;
            try
            {
                newWorker = new Worker(name, qualifications, salary);
                if (!Qualifications.ContainsAll(qualifications))
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

            Workers.Add(newWorker);
            AvailableWorkers.Add(newWorker);

            ICollection<Qualification> toRemove = new List<Qualification>();
            ICollection<Qualification> toAdd = new List<Qualification>();

            foreach (Qualification companyQual in Qualifications)
            {
                foreach (Qualification workerQual in qualifications)
                {
                    if (workerQual.Equals(companyQual))
                    {
                        toRemove.Add(companyQual);
                        Qualification newQual = companyQual;
                        newQual.AddWorker(newWorker);
                        toAdd.Add(newQual);
                    }
                }
            }

            foreach (Qualification qualification in toRemove)
            {
                Qualifications.Remove(qualification);
            }

            foreach (Qualification qualification in toAdd)
            {
                Qualifications.Add(qualification);
            }

            return newWorker;
        }

        public Qualification CreateQualification(string description)
        {
            Qualification newQualification;
            try
            {
                newQualification = new Qualification(description);
            }
            catch (Exception)
            {
                return null;
            }

            if (Qualifications.Add(newQualification))
            {
                return newQualification;
            }

            return null;
        }

        public Project CreateProject(string name, ICollection<Qualification> qualifications, ProjectSize size)
        {
            if (string.IsNullOrEmpty(name) || IsAllBlankSpace(name))
            {
                return null;
            }

            if (qualifications == null || qualifications.Count == 0)
            {
                return null;
            }

            if (size == null)
            {
                return null;
            }

            foreach (Qualification q in qualifications)
            {
                if (!Qualifications.Contains(q))
                {
                    return null;
                }
            }

            Project project = new Project(name, qualifications, size);
            Projects.Add(project);
            return project;
        }

        public void Start(Project project)
        {
            if (project.Status == ProjectStatus.ACTIVE || project.Status == ProjectStatus.FINISHED)
            {
                throw new ArgumentException("Project is not in a valid state to be started");
            }

            if (!Projects.Contains(project))
            {
                throw new ArgumentException("Project is not associated with this company");
            }

            if (project.MissingQualifications.Count > 0)
            {
                throw new ArgumentException("Project has missing qualifications and cannot be started");
            }

            project.Status = ProjectStatus.ACTIVE;
        }

        public void Finish(Project project)
        {
            if (project.Status == ProjectStatus.SUSPENDED || project.Status == ProjectStatus.FINISHED || project.Status == ProjectStatus.PLANNED)
            {
                throw new ArgumentException("Project is not active");
            }

            if (!Projects.Contains(project))
            {
                throw new ArgumentException("Project is not associated with this company");
            }

            ICollection<Worker> workersCopy = new List<Worker>(project.GetWorkers());

            foreach (Worker worker in workersCopy)
            {
                Unassign(worker, project);
            }

            project.Status = ProjectStatus.FINISHED;
        }

        public void Assign(Worker worker, Project project)
        {
            if (worker == null)
            {
                throw new ArgumentNullException("Worker must not be null");
            }

            if (project == null)
            {
                throw new ArgumentNullException("Project must not be null");
            }

            if (!Projects.Contains(project) || !Workers.Contains(worker))
            {
                throw new ArgumentException("Worker and Project must be associated with this company");
            }

            if (project.Status == ProjectStatus.ACTIVE || project.Status == ProjectStatus.FINISHED)
            {
                throw new ArgumentException("Project is not in a valid state to be assigned");
            }

            if (!AvailableWorkers.Contains(worker) || worker.GetProjects().Contains(project))
            {
                throw new ArgumentException("Worker must be available and not already assigned to the project");
            }

            if (!CanAssign(worker, project))
            {
                throw new ArgumentException("Cannot assign worker to the project");
            }

            AssignedWorkers.Add(worker);
            worker.AddProject(project);
            project.AddWorker(worker);

            if (worker.GetWorkload() == Worker.MAX_WORKLOAD)
            {
                AvailableWorkers.Remove(worker);
            }
        }

        private bool CanAssign(Worker worker, Project project)
        {
            return !worker.WillOverload(project) && project.IsHelpful(worker);
        }

        public void Unassign(Worker worker, Project project)
        {
            if (worker == null)
            {
                throw new ArgumentNullException("Worker must not be null");
            }

            if (project == null)
            {
                throw new ArgumentNullException("Project must not be null");
            }

            if (!Projects.Contains(project) || !Workers.Contains(worker))
            {
                throw new ArgumentException("Worker and Project must be associated with this company");
            }

            if (worker.GetProjects().Contains(project))
            {
                project.RemoveWorker(worker);
                worker.RemoveProject(project);
            }
            else
            {
                throw new ArgumentException("Worker is not assigned to the project");
            }

            if (worker.GetProjects().Count == 0)
            {
                AssignedWorkers.Remove(worker);
            }

            if (!AvailableWorkers.Contains(worker))
            {
                AvailableWorkers.Add(worker);
            }

            if (project.Status == ProjectStatus.ACTIVE && project.MissingQualifications.Count > 0)
            {
                project.Status = ProjectStatus.SUSPENDED;
            }
        }

        public void UnassignAll(Worker worker)
        {
            if (worker == null)
            {
                throw new ArgumentNullException("Worker must not be null");
            }

            if (!Workers.Contains(worker))
            {
                throw new ArgumentException("Worker is not associated with this company");
            }

            foreach (Project project in worker.GetProjects())
            {
                Unassign(worker, project);
            }
        }
    }
}
