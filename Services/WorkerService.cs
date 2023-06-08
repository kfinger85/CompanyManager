#nullable enable

using CompanyManager.Models;
using CompanyManager.Logging;

namespace CompanyManager.Services
{
    public class WorkerService
    {
        private readonly CompanyManagerContext _context;

        public WorkerService(CompanyManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of Worker entities that are associated with a specific Project entity.
        /// </summary>
        public IEnumerable<Worker> GetWorkersForProject(Project project)
        {
            return project.WorkerProjects.Select(wp => wp.Worker);
        }

        /// <summary>
        /// Retrieves a Worker entity with a specific Id from the Workers table of the database.
        /// </summary>
        public Worker? GetWorkerById(int id)
        {
            return _context.Workers.Find(id);
        }
        public Worker? GetWorkerByName(string name)
        {
            return _context.Workers.FirstOrDefault(w => w.Name == name);        
            
        }


        /// <summary>
        /// Retrieves all Worker entities in the Workers table of the database.
        /// </summary>
        public ICollection<Worker> GetWorkers()
        {
            return _context.Workers.ToList();
        }
        /// <summary>
        /// Retrieves a list of Qualifications that a specific Worker has for a given Project.
        /// </summary>
        public ICollection<Qualification> GetMatchingQualifications(Worker worker, Project project)
        {
            return worker.Qualifications.Intersect(project.Qualifications).ToList();
        }

        /// <summary>
        /// Checks if a specific Worker is assigned to a given Project.
        /// </summary>
        public bool IsWorkerAssignedToProject(Worker worker, Project project)
        {
            return worker.WorkerProjects.Any(wp => wp.Project == project);
        }

        /// <summary>
        /// Creates a new Worker entity with given attributes, adds it to the Workers table in the database, and saves the changes.
        /// </summary>
        public Worker CreateWorker(string name, ICollection<Qualification> qualifications, double salary, Company company, string username, string password)
        {
            if (salary < 0 || qualifications == null || qualifications.Count == 0 || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid arguments");
            }
        

            var worker = new Worker(name, qualifications, salary, company, username, password);


            _context.Workers.Add(worker);
            _context.SaveChanges();
            Logger.LogInformation($"Worker {worker.Name} was created");

            return worker;
        }

        /// <summary>
        /// Assigns a specific Worker to a given Project and removes the Project's missing qualifications that the Worker has.
        /// Saves the changes to the database.
        /// </summary>
        public bool AddProject(Worker worker, Project project, IEnumerable<Qualification> qualifications)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var workerProject = new WorkerProject(worker, project);
            worker.WorkerProjects.Add(workerProject);

            foreach (var qualification in qualifications)
            {
                var missingQualification = worker.MissingQualifications.FirstOrDefault(mq => mq.Id == qualification.Id);
                if (missingQualification != null)
                {
                    worker.MissingQualifications.Remove(missingQualification);
                }
            }

            _context.SaveChanges();
            Logger.LogInformation($"Project {project.Name} was created");
            return true;
        }
        /// <summary>
        /// Removes a specific Worker from a given Project and adds back the missing qualifications that the Worker had.
        /// Saves the changes to the database.
        /// </summary>
        public (bool, string) RemoveProject(Worker worker, Project project)
        {
            if (project == null)
            {
                Logger.LogError("Project must not be null");
                return (false, "Project must not be null");
            }

            var workerProject = worker.WorkerProjects.FirstOrDefault(wp => wp.Project == project);
            if (workerProject != null)
            {
                worker.WorkerProjects.Remove(workerProject);

                foreach (var qualification in worker.Qualifications)
                {
                    var missingQualification = project.MissingQualifications.FirstOrDefault
                        (mq => mq.QualificationId == qualification.Id && mq.ProjectId == project.Id);
                
                    if (missingQualification != null)
                    {
                        project.MissingQualifications.Add(missingQualification);

                    }
                }
                _context.SaveChanges();
                Logger.LogInformation($"Project {project.Name} was removed");   
                return (true, string.Empty);
            }
            return (false, "Project not found");
        }

        /// <summary>
        /// Sets a new username for a specific Worker and saves the changes to the database.
        /// </summary>
        public (bool, string) SetUsername(Worker worker, string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return (false, "Username must not be null or empty");
            }

            worker.Username = username;
            _context.SaveChanges();

            return (true, string.Empty);
        }

        /// <summary>
        /// Sets a new password for a specific Worker and saves the changes to the database.
        /// </summary>
        public (bool, string) SetPassword(Worker worker, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return (false, "Password must not be null or empty");
            }

            worker.Password = password;
            _context.SaveChanges();

            return (true, string.Empty);
        }
    }
}
