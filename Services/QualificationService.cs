using CompanyManager.Models;
namespace CompanyManager.Services
{
    public class QualificationService
    {
        private readonly CompanyManagerContext _context;

        public QualificationService(CompanyManagerContext context)
        {
            _context = context;
        }

        public Qualification GetByName(string name)
        {
            return _context.Qualifications.Find(name);
        }

        public  ICollection<Qualification> GetQualifications()
        {
            return _context.Qualifications.ToList();
        }

        public Qualification CreateQualification(string description)
        {
            if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Description must not be null or empty");
            }

            var qualification = new Qualification
            {
                Name = description,
            };

            _context.Qualifications.Add(qualification);
            _context.SaveChanges();
            
            return qualification;
        }

        public bool AddWorker(Qualification qualification, Worker worker)
        {
            if (worker == null)
            {
                throw new ArgumentNullException("Worker must not be null");
            }
            
            qualification.Workers.Add(worker);
            _context.SaveChanges();
            return true;
        }

        public bool RemoveWorker(Qualification qualification, Worker worker)
        {
            if (worker == null)
            {
                throw new ArgumentNullException("Worker must not be null");
            }
            
            qualification.Workers.Remove(worker);
            _context.SaveChanges();
            return true;
        }
    }
}
