using CompanyManager.Models;

namespace CompanyManager.Models.DTO
{
    public class AssignmentDTO
    {
        public string Worker { get; set; }
        public string Project { get; set; }

        public AssignmentDTO() { }

        public AssignmentDTO(string worker, string project)
        {
            Worker = worker;
            Project = project;
        }

        public AssignmentDTO SetWorker(string worker) // renamed method
        {
            Worker = worker;
            return this;
        }

        public AssignmentDTO SetProject(string project) // renamed method
        {
            Project = project;
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (!(obj is AssignmentDTO))
                return false;
            AssignmentDTO assignmentDTO = (AssignmentDTO)obj;
            return Worker == assignmentDTO.Worker && Project == assignmentDTO.Project;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Worker, Project);
        }

        public override string ToString()
        {
            return $"{{ worker='{Worker}', project='{Project}' }}";
        }
    }
}
