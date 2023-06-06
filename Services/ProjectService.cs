using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;
namespace CompanyManager.Services
{
    public class ProjectService
    {
        private readonly CompanyManagerContext _context;
        
        public ProjectService(CompanyManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }

        public Project CreateProject(string name, ICollection<Qualification> qualifications, ProjectSize size ,Company company)
        {
            if (qualifications == null || qualifications.Count == 0 || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid arguments");
            }

            var project = new Project(name, qualifications, size, company);

            _context.Projects.Add(project);
            _context.SaveChanges();

            return project;
        }

        public IEnumerable<Qualification> GetQualifications(Project project)
        {
            var qualifications = _context.Projects
                .Include(p => p.Qualifications)
                .Where(p => p.Id == project.Id)
                .SelectMany(p => p.Qualifications)
                .ToList();

            return qualifications;
        }

        public bool IsWorkerAssignedToProject(Worker worker, Project project)
        {
            return worker.WorkerProjects.Any(wp => wp.Project == project);
        }

        public (bool, string) AddWorker(Project project, Worker worker)
        {
            if (worker == null)
            {
                return (false, "Worker must not be null");
            }
            project.Workers.Add(worker);
            

            foreach (var qualification in worker.Qualifications)
            {
                var missingQualification = project.MissingQualifications.FirstOrDefault
                            (mq => mq.QualificationId == qualification.Id && mq.ProjectId == project.Id);

                if(missingQualification != null)
                {
                    project.MissingQualifications.Remove(missingQualification);
                }            
        
        }
            _context.SaveChanges();
            return (true, string.Empty);
        }

        public (bool, string) RemoveWorker(Project project, Worker worker)
        {
            if (worker == null)
            {
                return (false, "Worker must not be null");
            }
            project.Workers.Remove(worker);
            foreach (var qualification in project.Qualifications)
            {
                if (!project.Workers.Any(w => w.Qualifications.Contains(qualification)))
                {
                    var missingQualification = new MissingQualification(project, qualification, qualification.Name);
                    project.MissingQualifications.Add(missingQualification);
                }
            }
            _context.SaveChanges();
            return (true, string.Empty);
        }

        public (bool, string) AddQualification(Project project, Qualification qualification)
        {
            if (qualification == null)
            {
                return (false, "Null Qualification");
            }
            project.Qualifications.Add(qualification);
            if (project.Status == ProjectStatus.ACTIVE && GetMissingQualifications(project).Count > 0)
            {
                project.Status = ProjectStatus.SUSPENDED;
            }

            MissingQualification missingQualification = new MissingQualification
            {
                Name = qualification.Name,
                Project = project,
                Qualification = qualification
            };

            if (project.Status == ProjectStatus.ACTIVE)
            {
                var missingQualificationInProject = project.MissingQualifications.FirstOrDefault
                    (mq => mq.QualificationId == qualification.Id && mq.ProjectId == project.Id);
                if(missingQualificationInProject != null)
                {
                    project.MissingQualifications.Remove(missingQualificationInProject);
                }
            }
            _context.SaveChanges();
            return (true, string.Empty);
        }

        public ICollection<Qualification> GetMissingQualifications(Project project)
        {
            ICollection<Qualification> missingQualifications = new HashSet<Qualification>();
            foreach (var qualification in project.Qualifications)
            {
                bool missing = true;
                foreach (var worker in project.Workers)
                {
                    if (worker.Qualifications.Any(q => q.Name == qualification.Name)) 
                    {
                        missing = false;
                        break;
                    }
                }
                if (missing)
                {
                    missingQualifications.Add(qualification);
                }
            }
            return missingQualifications;
        }

        public bool WorkerIsHelpful(Project project)
        {
            if (project == null)
            {
                return false;
            }

            foreach (var qualification in project.Qualifications)
            {
                var missingQualification = project.MissingQualifications.FirstOrDefault
                    (mq => mq.QualificationId == qualification.Id && mq.ProjectId == project.Id);
                
                if (missingQualification != null)
                {
                    if (project.MissingQualifications.Contains(missingQualification))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
