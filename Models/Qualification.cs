using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CompanyManager.Models.DTO;

namespace CompanyManager.Models
{
    public class Qualification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(name: "name")]
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();

        public virtual ICollection<Worker> Workers {get; set; }= new HashSet<Worker>();
        public virtual ICollection<Company> Companies { get; set; } = new HashSet<Company>();

        // public virtual ICollection<WorkerQualification> WorkerQualifications { get; set; } = new HashSet<WorkerQualification>();

        protected Qualification() 
        { 
        }

        public Qualification(string description)
        {
            if (string.IsNullOrEmpty(description) || IsAllBlankSpace(description))
            {
                throw new ArgumentException("Description must not be null or empty");
            }
            Workers = new HashSet<Worker>();
            Name = description;
        }

        public void SetName(string description)
        {
            if (string.IsNullOrEmpty(description) || IsAllBlankSpace(description))
            {
                throw new ArgumentException("Description must not be null or empty");
            }
            Name = description;
        }

        private bool IsAllBlankSpace(string description)
        {
            return string.IsNullOrWhiteSpace(description);
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }
            if (GetType() != other.GetType())
            {
                return false;
            }
            Qualification q = (Qualification)other;
            return q.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public IEnumerable<Worker> GetWorkers()
        {
            return new HashSet<Worker>(Workers);
        }

        public void AddWorker(Worker worker)
        {
            if (worker == null)
            {
                throw new ArgumentNullException("Worker must not be null");
            }
            Workers.Add(worker);
        }

        public void RemoveWorker(Worker worker)
        {
            if (worker == null)
            {
                throw new ArgumentNullException("Worker must not be null");
            }
            Workers.Remove(worker);
        }

        public QualificationDTO ToDTO()
        {
            return new QualificationDTO(
                Name,
                Workers.Select(worker => worker.Name).ToArray()
            );
        }
    }
}
