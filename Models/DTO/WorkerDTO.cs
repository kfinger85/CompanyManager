using System;
using System.Linq;

namespace CompanyManager.Models.DTO
{
    public class WorkerDTO
    {
        public string Name { get; set; }
        public double Salary { get; set; }
        public int Workload { get; set; }
        public string[] Projects { get; set; }
        public string[] Qualifications { get; set; }

        public WorkerDTO() { }

        public WorkerDTO(string name, double salary, int workload, string[] projects, string[] qualifications)
        {
            Name = name;
            Salary = salary;
            Workload = workload;
            Projects = projects;
            Qualifications = qualifications;
        }

        public WorkerDTO SetName(string name)
        {
            Name = name;
            return this;
        }

        public WorkerDTO SetSalary(double salary)
        {
            Salary = salary;
            return this;
        }

        public WorkerDTO SetWorkload(int workload)
        {
            Workload = workload;
            return this;
        }

        public WorkerDTO SetProjects(string[] projects)
        {
            Projects = projects;
            return this;
        }

        public WorkerDTO SetQualifications(string[] qualifications)
        {
            Qualifications = qualifications;
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (!(obj is WorkerDTO))
                return false;
            WorkerDTO workerDTO = (WorkerDTO)obj;
            return Name == workerDTO.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"{{ name='{Name}', salary='{Salary}', workload='{Workload}', projects='[{string.Join(",", Projects)}]', qualifications='[{string.Join(",", Qualifications)}]' }}";
        }
    }
}
