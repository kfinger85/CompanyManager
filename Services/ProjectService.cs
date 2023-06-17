using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;
using CompanyManager.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace CompanyManager.Services
{
    public class ProjectService : IProjectService
    {
        private readonly CompanyManagerContext _context;
        private readonly WorkerService _workerService;
        private readonly ILogger<ProjectService> _logger;

        
        public ProjectService(CompanyManagerContext context, WorkerService workerService, ILogger<ProjectService> logger)
        {
            _context = context;
            _workerService = workerService;
            _logger = logger;
        }

        /// <summary>
        /// Fetches all projects and returns them as a list of type ProjectDTOs.
        /// </summary>
        public ICollection<Project.ProjectDTO> FetchAllProjectDTOs()
        {
            var projects = _context.Projects
                .Select(p => new Project.ProjectDTO {
                    MissingQualifications = p.MissingQualifications.Select(mq => mq.Name),
                    Size = p.Size.ToString(),
                    Name = p.Name,
                    Status = p.Status.ToString(),
                    Qualifications = p.Qualifications.Select(q => q.Name),
                    Workers = _context.WorkerProject
                        .Where(wp => wp.ProjectId == p.Id)
                        .Select(wp => wp.Worker.Name)
                        .ToList()
                })
                .ToList();

                return projects;
        }
        /// <summary>
        /// Fetches all projects with return type Collections.Generic.List<Project>.
        /// </summary>
        public IEnumerable<Project> FetchAllProjects()
        {
            return _context.Projects.ToList();
        }


        public Project GetProjectByName(string name)
        {
            return _context.Projects
                .Include(p => p.Company)
                .Include(p => p.Qualifications)
                .Include(p => p.MissingQualifications)
                .Include(p => p.WorkerProjects)
                    .ThenInclude(wp => wp.Worker)
                .FirstOrDefault(p => p.Name == name);
        }
        public Project CreateProject(string name, ICollection<Qualification> qualifications, ProjectSize size ,Company company)
        {
            try
            {
                if (qualifications == null || qualifications.Count == 0 || string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Invalid arguments");
                }

                var project = new Project(name, qualifications, size, company);
                _context.Projects.Add(project); // You need to add the project to your DbContext

                _context.SaveChanges();
                Logger.LogInformation($"Project {project.Name} was created");   

                return project;
            }
            catch(Exception e)
            {
                Logger.LogError(e.Message);
                return null;
            }
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
            Logger.LogInformation($"Worker {worker.Name} was added to project {project.Name}");
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
            Logger.LogInformation($"Worker {worker.Name} was removed from project {project.Name}");
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
            Logger.LogInformation($"Qualification {qualification.Name} was added to project {project.Name}");
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
                        Logger.LogInformation($"Worker is helpful with qualification {missingQualification.Name} for project {project.Name}");
                        return true;
                    }
                }
            }
            Logger.LogInformation($"Worker is not helpful for project {project.Name}");
            return false;
        }
        public IActionResult UnassignWorkerFromProject(Project projectToUnassignFrom, Worker workerToUnassign)
        {
            if (projectToUnassignFrom == null || workerToUnassign == null)
            {
                _logger.LogInformation($"Project {projectToUnassignFrom?.Name} or worker {workerToUnassign?.Name} not found.");
                return new NotFoundObjectResult("Project or worker not found.");
            }

            var workersProject = _workerService.GetWorkersProject(workerToUnassign, projectToUnassignFrom);

            if (workersProject == null)
            {
                _logger.LogInformation($"Worker {workerToUnassign.Name} is not assigned to project {projectToUnassignFrom.Name}.");
                return new NotFoundObjectResult("Worker not assigned to project.");
            }

            foreach (var qualification in workerToUnassign.Qualifications)
            {
                // Check if the qualification is already present in project's qualifications
                var projectQualification = projectToUnassignFrom.Qualifications.FirstOrDefault(q => q.Id == qualification.Id);
                if (projectQualification != null)
                {
                    // Check if any remaining assigned workers satisfy the qualification
                    bool isQualificationSatisfied = projectToUnassignFrom.WorkerProjects
                        .Where(wp => wp.WorkerId.Equals(workerToUnassign.Id))
                        .Any(wp => wp.Worker.Qualifications.Any(q => q.Id == qualification.Id));

                    if (!isQualificationSatisfied)
                    {
                        // Add the missing qualification if none of the remaining assigned workers satisfy it
                        _logger.LogInformation($"Adding missing qualification {qualification.Name} to project {projectToUnassignFrom.Name} because worker {workerToUnassign.Name} was unassigned.");
                        projectToUnassignFrom.MissingQualifications.Add(new MissingQualification(projectToUnassignFrom, qualification, qualification.Name));
                    }
                }
            }

            _context.WorkerProject.Remove(workersProject);
            _context.SaveChanges();

            return new OkResult();
        }
    }
}
