using CompanyManager.Models;
using CompanyManager.Services;
using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace CompanyManager
{
    public class CompanyInitializer
    {
        private CompanyManager.Models.Company _company;
        private readonly CompanyManagerContext _context;
        private readonly CompanyService _companyService;
        private readonly QualificationService _qualificationService;
        private readonly ProjectService _projectService;
        private readonly WorkerService _workerService;

        public CompanyInitializer(CompanyManagerContext context)
        {
            _context = context;
            _companyService = new CompanyService(_context);
            _qualificationService = new QualificationService(_context);
            _projectService = new ProjectService(_context);
            _workerService = new WorkerService(_context);
        }

        public void Initialize()
        {
            try{
            var faker = new Bogus.Faker();
            _company = _companyService.CreateCompany("Test Company");
             var qualifications = new[] { "Java", "C#", "Python"};
            foreach (var qualificationName in qualifications)
            {
                _qualificationService.CreateQualification(qualificationName);
            }

            var projectQualifications = _qualificationService.GetQualifications();

            Project project = _projectService.CreateProject("Test Project", projectQualifications, ProjectSize.SMALL, _company);  
            Project project2 = _projectService.CreateProject("Test Project Missing", projectQualifications, ProjectSize.SMALL, _company);  

            string name = "Kevin Finger";
            string username = name.Split(' ')[0].ToLower() + name.Split(' ')[1].ToLower() + faker.Random.Int(1, 1000);
            
            var qualificationsObjects = _qualificationService.GetQualifications();

            Worker worker = _companyService.CreateWorker(name, qualificationsObjects, 696969.69, username, "password");
            _companyService.AssignWorkerToProject(worker, project);
            /*
            ExecuteInTransaction(CreateCompany);
            ExecuteInTransaction(CreateQualification);
            ExecuteInTransaction(CreateProject);
            ExecuteInTransaction(CreateWorker);
            ExecuteInTransaction(AssignWorkerToProject);
            */

            foreach (var entry in _context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    Console.WriteLine($"Entity of type {entry.Entity.GetType().Name} was added.");
                }
                else if (entry.State == EntityState.Modified)
                {
                    Console.WriteLine($"Entity of type {entry.Entity.GetType().Name} was modified.");
                }
                else if (entry.State == EntityState.Deleted)
                {
                    Console.WriteLine($"Entity of type {entry.Entity.GetType().Name} was deleted.");
                }
            }

            }catch{
                    Debug.WriteLine("Error");
            }finally{
              //  _context.Database.EnsureDeleted(); 
            }
        }

        private void ExecuteInTransaction(Action method)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    method();
                    // _context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw; // Re-throw the exception to propagate it further
                }
            }
        }

        private void CreateCompany()
        {
            var faker = new Bogus.Faker();
            _companyService.CreateCompany(faker.Company.CompanyName());
        }

        private void CreateQualification()
        {
            var faker = new Bogus.Faker();
            var qualifications = new[] { "Java", "C#", "Python", "JavaScript", "Ruby", "HTML", "CSS", "PHP", "Swift", "Go", "Rust" };

            foreach (var qualificationName in qualifications)
            {
                _qualificationService.CreateQualification(qualificationName);
            }
        }

        private void CreateProject()
        {
            var faker = new Bogus.Faker();
            var qualifications = _qualificationService.GetQualifications();

            // Get a random qualification
            var randomQualifications = qualifications.OrderBy(q => faker.Random.Int()).Take(3).ToList();
            ProjectSize randomSize = faker.PickRandom<ProjectSize>();
            _companyService.CreateProject(faker.Company.CompanyName(), randomQualifications, randomSize);

        }

        private void CreateWorker()
        {
            var faker = new Bogus.Faker();
            var qualifications = _qualificationService.GetQualifications();
            var randomQualifications = qualifications.OrderBy(q => faker.Random.Int()).Take(3).ToList();
            string name = faker.Name.FullName();
            string username = name.Split(' ')[0].ToLower() + name.Split(' ')[1].ToLower() + faker.Random.Int(1, 1000);
            _companyService.CreateWorker(name, randomQualifications, faker.Random.Int(10000, 100000), username, faker.Internet.Password());
        }

        private void AssignWorkerToProject()
        {
            var projects = _projectService.GetProjects();
            var workers = _workerService.GetWorkers();

            // Loop through all the projects
            foreach (var project in projects)
            {
                // Loop through all the workers
                foreach (var worker in workers)
                {
                    // Check if the worker has at least one qualification that the project needs
                    var matchingQualifications = _workerService.GetMatchingQualifications(worker, project);

                    if (matchingQualifications.Count > 0)
                    {
                        // If the worker has at least one matching qualification, assign the worker to the project
                        // but first, check if the association already exists
                        if (!_projectService.IsWorkerAssignedToProject(worker, project))
                        {
                            _projectService.AddWorker(project, worker);
                        }
                        else
                        {
                            Console.WriteLine($"The combination of WorkerId {worker.Id} and ProjectId {project.Id} already exists in the WorkerProject table.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Worker {worker.Id} does not have the qualifications required for Project {project.Id}.");
                    }
                }
            }
        }
    }
}
