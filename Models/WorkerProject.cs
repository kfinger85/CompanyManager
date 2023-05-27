using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManager.Models
{
    [Table(name: "worker_project")]
    public class WorkerProject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [ForeignKey("Worker")]
        public long WorkerId { get; set; }
        public virtual Worker Worker { get; set; }

        [Required]
        [ForeignKey("Project")]
        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [Column(name: "date_assigned")]
        public string DateAssigned { get; set; }

        public WorkerProject() { }

        public WorkerProject(Worker worker, Project project)
        {
            Worker = worker;
            Project = project;
            DateAssigned = DateTime.Now.ToString("yyyy-MM-dd");
        }

        public Project GetProject()
        {
            return Project;
        }
    }
}
