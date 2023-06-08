#nullable disable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManager.Models
{
    public class Worker
    {
        public static readonly int MAX_WORKLOAD = 12;
        public long Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual ICollection<WorkerProject> WorkerProjects { get; set; } = new HashSet<WorkerProject>();
        public virtual ICollection<Qualification> Qualifications { get; set; } = new HashSet<Qualification>();
        [NotMapped]
        public virtual ICollection<Qualification> MissingQualifications { get; set; } = new HashSet<Qualification>();
        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public bool IsAvailable => WorkerProjects.Count < MAX_WORKLOAD;

        public Worker(string name, ICollection<Qualification> qualifications, double salary, Company company,String username, 
                            String password)
        {
            Name = name;
            Qualifications = qualifications;
            Salary = salary;
            Company = company;
            Username = username;
            Password = password;
        }

        // The warning you're getting is from a non-nullable reference types feature that was added in C# 8.0. The compiler is warning you that you haven't assigned a value to the Companies property in the constructor. 
        // However, in this specific case, you can safely ignore the warning because Entity Framework Core assigns a value to it at runtime.
        
        public Worker()
        {
        }


    }
}
