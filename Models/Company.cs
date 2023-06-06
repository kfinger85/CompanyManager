using System.Text.RegularExpressions;
namespace CompanyManager.Models
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Worker> Workers { get; set; } = new HashSet<Worker>();
        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
        public virtual ICollection<Qualification> Qualifications { get; set; } = new HashSet<Qualification>();

        public Company()
        {
        }

        public Company(string name)
        {
            if (string.IsNullOrEmpty(name) || IsAllBlankSpace(name))
            {
                throw new ArgumentException("Name must not be null or empty");
            }
            Name = name;
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
            return $"{Name}:{Workers.Count}:{Projects.Count}";
        }
        public Worker CreateWorker(string name, ICollection<Qualification> qualifications, double salary, string username, string password)
        {
            if (string.IsNullOrEmpty(name) || IsAllBlankSpace(name))
            {
                throw new ArgumentException("Name must not be null or empty");
            }

            if (qualifications == null)
            {
                throw new ArgumentNullException(nameof(qualifications));
            }

            if (string.IsNullOrEmpty(username) || IsAllBlankSpace(username))
            {
                throw new ArgumentException("Username must not be null or empty");
            }

            if (string.IsNullOrEmpty(password) || IsAllBlankSpace(password))
            {
                throw new ArgumentException("Password must not be null or empty");
            }

            if (salary < 0)
            {
                throw new ArgumentException("Salary must be greater than or equal to 0");
            }

            Worker newWorker = new Worker(name, qualifications, salary, this, username, password);

            ICollection<Qualification> toRemove = new List<Qualification>();
            ICollection<Qualification> toAdd = new List<Qualification>();

            foreach (Qualification companyQual in Qualifications)
            {
                foreach (Qualification workerQual in qualifications)
                {
                    if (workerQual.Equals(companyQual))
                    {
                        toRemove.Add(companyQual);
                        companyQual.AddWorker(workerQual, newWorker);
                        toAdd.Add(companyQual);
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

            Workers.Add(newWorker);
            return newWorker; // Return the newly created worker

        }

    }
}
