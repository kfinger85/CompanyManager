
namespace CompanyManager.Models
{
    public class Qualification
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
        public ICollection<Worker> Workers { get; set; } = new HashSet<Worker>();

        public Qualification()
        {
        }
        public Qualification(string name)
        {
            Name = name;
        }
        public static Qualification GetQualification(string name)
        {
            return new Qualification(name);
        }
        public void AddWorker(Qualification workerQualification, Worker worker)
        {
            if (workerQualification == null)
            {
                throw new ArgumentNullException(nameof(workerQualification));
            }

            if (worker == null)
            {
                throw new ArgumentNullException(nameof(worker));
            }

            if (!workerQualification.Equals(this))
            {
                throw new ArgumentException("The provided qualification is different from this qualification.");
            }

            if (Workers.Contains(worker))
            {
                throw new ArgumentException("The worker is already associated with this qualification.");
            }

            worker.Qualifications.Add(this);
            Workers.Add(worker);
        }
    }
}
