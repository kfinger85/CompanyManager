using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManager.Models
{
    public class WorkerProject
    {

        public long WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [Column(name: "date_assigned")]
        public string DateAssigned { get; set; }

        public WorkerProject() { }

        public WorkerProject(Worker worker, Project project)
        {
            Worker = worker;
            Project = project;
            DateAssigned = DateTime.Now.ToString("yyyy-MM-dd:HH:mm:ss");
        }

        public Project GetProject()
        {
            return Project;
        }
    }
}
