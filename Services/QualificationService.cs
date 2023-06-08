using CompanyManager.Models;
using CompanyManager.Logging;

namespace CompanyManager.Services
{
    public class QualificationService
    {
        private readonly CompanyManagerContext _context;

        public QualificationService(CompanyManagerContext context)
        {
            _context = context;
        }
        #nullable enable
        public Qualification? GetByName(string name)
        {
            Logger.LogInformation($"Getting qualification by name: {name}");
            return _context.Qualifications.Find(name);
        }

        public ICollection<Qualification> GetQualifications()
        {
            Logger.LogInformation("Getting all qualifications");
            return _context.Qualifications.ToList();
        }

        public Qualification CreateQualification(string description)
        {
            Logger.LogInformation($"Creating qualification with description: {description}");

            if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
            {
                Logger.LogWarning("Description must not be null or empty");
                throw new ArgumentException("Description must not be null or empty");
            }

            var existingQualification = _context.Qualifications.FirstOrDefault(q => q.Name == description);

            if (existingQualification != null)
            {
                Logger.LogInformation($"Qualification with description {description} already exists");
                return existingQualification;
            }

            var qualification = new Qualification
            {
                Name = description,
            };

            _context.Qualifications.Add(qualification);
            _context.SaveChanges();

            Logger.LogInformation($"Created qualification with description: {description}");
            return qualification;
        }

        public bool AddWorker(Qualification qualification, Worker worker)
        {
            Logger.LogInformation($"Adding worker to qualification: {qualification.Name}");

            if (worker == null)
            {
                Logger.LogWarning("Worker must not be null");
                throw new ArgumentNullException("Worker must not be null");
            }

            qualification.Workers.Add(worker);
            _context.SaveChanges();

            Logger.LogInformation($"Added worker to qualification: {qualification.Name}");
            return true;
        }

        public bool RemoveWorker(Qualification qualification, Worker worker)
        {
            Logger.LogInformation($"Removing worker from qualification: {qualification.Name}");

            if (worker == null)
            {
                Logger.LogWarning("Worker must not be null");
                throw new ArgumentNullException("Worker must not be null");
            }

            qualification.Workers.Remove(worker);
            _context.SaveChanges();

            Logger.LogInformation($"Removed worker from qualification: {qualification.Name}");
            return true;
        }
    }
}
