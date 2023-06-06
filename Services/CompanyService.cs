using System.Text.RegularExpressions;
using CompanyManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Services
{
    public class CompanyService
    {
        private readonly CompanyManagerContext _context;

        private readonly ProjectService _projectService;

        private Company _company;

        public CompanyService(CompanyManagerContext context)
        {
            _context = context;
            _projectService = new ProjectService(_context);

        }

        public Company GetCompanyByName(string name)
        {
            return _context.Companies.FirstOrDefault(c => c.Name == name);
        }

        public Company CreateCompany(string name)
        {
            if (string.IsNullOrEmpty(name) || IsAllBlankSpace(name))
            {
                throw new ArgumentException("Company name must not be null or blank");
            }

            if (_context.Companies.Any(c => c.Name == name))
            {
                throw new ArgumentException("Company name must be unique");
            }

            _company = new Company(name);
            _context.Companies.Add(_company);
            _context.SaveChanges();

            return _company;
        }

        public void UpdateCompany(Company company)
        {
            // Update the company in the context and save changes
            _context.Companies.Update(company);
            _context.SaveChanges();
        }


        public (bool success, string message) CreateQualification(string description)
        {
            try
            {
                Qualification newQualification = new Qualification(description);
                _context.Qualifications.Add(newQualification);
                return (true, "Qualification created successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool success, string message) CreateProject(string name, ICollection<Qualification> qualifications, ProjectSize size)
        {
            try
            {
                if (string.IsNullOrEmpty(name) || IsAllBlankSpace(name))
                {
                    return (false, "Project name must not be null or blank");
                }

                if (qualifications == null || qualifications.Count == 0)
                {
                    return (false, "Project must have qualifications");
                }

                foreach (Qualification q in qualifications)
                {
                    if (!_context.Qualifications.Contains(q))
                    {
                        return (false, "All project qualifications must be recognized by the company");
                    }
                }

                Project project = new Project(name, qualifications, size, _company);
                _context.Projects.Add(project);
                _context.SaveChanges();
                return (true, "Project created successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public Worker CreateWorker(string name, ICollection<Qualification> qualifications, double salary, string username, string password)
        {
            if (string.IsNullOrEmpty(name) || IsAllBlankSpace(name))
            {
                throw new ArgumentException("Worker name must not be null or blank");
            }

            if (qualifications == null || qualifications.Count == 0)
            {
                throw new ArgumentException("Worker must have qualifications");
            }

            foreach (Qualification q in qualifications)
            {
                if (!_context.Qualifications.Contains(q))
                {
                    throw new ArgumentException("All worker qualifications must be recognized by the company");
                }
            }

            if (string.IsNullOrEmpty(username) || IsAllBlankSpace(username))
            {
                throw new ArgumentException("Worker username must not be null or blank");
            }

            if (string.IsNullOrEmpty(password) || IsAllBlankSpace(password))
            {
                throw new ArgumentException("Worker password must not be null or blank");
            }

            Worker worker = new Worker(name, qualifications, salary, _company, username, password);
            _context.Workers.Add(worker);
            _context.SaveChanges();
            return worker;
        }

        public bool AssignWorkerToProject(Worker worker, Project project)
        {
            if (worker == null)
            {
                throw new ArgumentException("Worker must not be null");
            }

            if (project == null)
            {
                throw new ArgumentException("Project must not be null");
            }

            if (worker.Company != project.Company)
            {
                throw new ArgumentException("Worker must be from the same company as the project");
            }

            if (project.Company != worker.Company)
            {
                throw new ArgumentException("Project must be from the same company as the worker");
            }

            if (worker.WorkerProjects.Any(wp => wp.Project == project))
            {
                throw new ArgumentException("Worker is already assigned to project");
            }

            foreach (Qualification q in project.Qualifications)
            {
                if (!worker.Qualifications.Contains(q))
                {
                    throw new ArgumentException("Worker does not have the required qualifications to be assigned to project");
                }
            }
        foreach (Qualification q in worker.Qualifications)
        {
            var missingQualification = project.MissingQualifications
                .FirstOrDefault(mq => mq.QualificationId == q.Id && mq.ProjectId == project.Id);
            if (missingQualification != null)
            {
                project.MissingQualifications.Remove(missingQualification);
            }
        }

            WorkerProject workerProject = new WorkerProject(worker, project);
            worker.WorkerProjects.Add(workerProject);
            _context.WorkerProject.Add(workerProject); // Add WorkerProject to the context
            _context.SaveChanges();
            return true;
        }

        public bool UnassignWorkerFromProject(Worker worker, Project project)
        {
            if (worker == null)
            {
                throw new ArgumentException("Worker must not be null");
            }

            if (project == null)
            {
                throw new ArgumentException("Project must not be null");
            }

            if (worker.Company != project.Company)
            {
                throw new ArgumentException("Worker must be from the same company as the project");
            }

            if (project.Company != worker.Company)
            {
                throw new ArgumentException("Project must be from the same company as the worker");
            }

            if (!worker.WorkerProjects.Any(wp => wp.Project == project))
            {
                throw new ArgumentException("Worker is not assigned to project");
            }
            WorkerProject workerProject = worker.WorkerProjects.FirstOrDefault(wp => wp.Project == project);
            
            foreach (Qualification q in worker.Qualifications)
            {
                var missingQualification = MissingQualification.QualificationToMissingQualification(project, q);
                if (!project.MissingQualifications.Contains(q))
                {
                    var missingQualification = new MissingQualification(project, q, q.Name); 
                    project.MissingQualifications.Add(missingQualification);
                }
            }
            
            worker.WorkerProjects.Remove(workerProject);
            _context.WorkerProject.Remove(workerProject); // Remove WorkerProject from the context
            _context.SaveChanges();
            return true;
        }

        
        

        private bool IsAllBlankSpace(string name)
        {
            Regex pattern = new Regex("^\\s*$");
            return pattern.IsMatch(name);
        }
    }
}
