using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CompanyManager.Models.DTO;
namespace CompanyManager.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ProjectSize Size { get; set; }

        [Required]
        public ProjectStatus Status { get; set; }

        public virtual ICollection<Worker> Workers { get; set; } = new HashSet<Worker>();

        public virtual ICollection<Qualification> Qualifications { get; set; } = new HashSet<Qualification>();
   
        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }

        // This exists to make EF happy
        public virtual ICollection<WorkerProject> WorkerProjects { get; set; }

        public ICollection<Qualification> missingQualifications; 

        public Project() { }

        public Project(string name, ICollection<Qualification> qualifications, ProjectSize size)
        {
            CheckArgValidity(name, qualifications, size);
            Name = name;
            Qualifications = qualifications;
            Size = size;
            Status = ProjectStatus.PLANNED;
        }

        private void CheckArgValidity(string name, ICollection<Qualification> qualifications, ProjectSize size)
        {
            if (qualifications == null || qualifications.Count == 0)
            {
                throw new ArgumentException("Project must have at least one qualification");
            }
            if (size == null)
            {
                throw new ArgumentException("Project must have a size");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name field must not be null or empty");
            }
        }

        public long GetId()
        {
            return Id;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetQualifications(ICollection<Qualification> qualifications)
        {
            Qualifications = qualifications;
        }

        public void SetSize(ProjectSize size)
        {
            Size = size;
        }

        private void ThrowArgException(string reason)
        {
            throw new ArgumentException(reason);
        }

        public override bool Equals(object? other)
        {
            if (other == null || GetType() != other.GetType())
            {
                return false;
            }
            return Name == ((Project)other).Name;
        }


        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name}:{Workers.Count}:{Status}";
        }

        public string GetName()
        {
            return Name;
        }

        public ProjectSize GetSize()
        {
            return Size;
        }

        public ProjectStatus GetStatus()
        {
            return Status;
        }

        public void SetStatus(ProjectStatus status)
        {
            if (status == null)
            {
                throw new ArgumentException("Status must not be null");
            }
            Status = status;
        }

        public void AddWorker(Worker worker)
        {
            if (worker == null)
            {
                throw new ArgumentException("Worker must not be null");
            }
            Workers.Add(worker);
        }

        public void RemoveWorker(Worker worker)
        {
            if (worker == null)
            {
                throw new ArgumentException("Worker must not be null");
            }
            Workers.Remove(worker);
        }

        public ICollection<Worker> GetWorkers()
        {
            return Workers;
        }

        public void RemoveAllWorkers()
        {
            Workers.Clear();
        }

        public ICollection<Qualification> GetRequiredQualifications()
        {
            return Qualifications;
        }

        public void AddQualification(Qualification qualification)
        {
            if (qualification == null)
            {
                throw new ArgumentException("Null Qualification");
            }
            Qualifications.Add(qualification);
            if (Status == ProjectStatus.ACTIVE && GetMissingQualifications().Count > 0) // Updated line
            {
                Status = ProjectStatus.SUSPENDED;
            }
        }

        public ICollection<Qualification> GetMissingQualifications()
        {
            ICollection<Qualification> missingQualifications = new HashSet<Qualification>();
            foreach (var qualification in Qualifications)
            {
                bool missing = true;
                foreach (var worker in Workers)
                {
                    if (worker.Qualifications.Any(q => q.Name == qualification.Name)) // Updated line
                    {
                        missing = false;
                        break;
                    }
                }
                if (missing)
                {
                    missingQualifications.Add(qualification);
                }
            }
            return missingQualifications;
        }


        public bool IsHelpful(Worker worker)
        {
            if (worker == null)
            {
                return false;
            }
            foreach (var qual in worker.GetQualifications())
            {
                if (GetMissingQualifications().Contains(qual))
                {
                    return true;
                }
            }
            return false;
        }

        public ProjectDTO ToDTO()
        {
            var qualificationStrings = Qualifications.Select(qual => qual.ToDTO().Description).ToArray();
            var missingQualificationStrings = GetMissingQualifications().Select(qual => qual.ToDTO().Description).ToArray();
            var workerStrings = Workers.Select(worker => worker.Name).ToArray();

            return new ProjectDTO(Name, Size, Status, workerStrings, qualificationStrings, missingQualificationStrings);
        }

        public Project OrElseThrow(object obj)
        {
            return null;
        }
    }
}
