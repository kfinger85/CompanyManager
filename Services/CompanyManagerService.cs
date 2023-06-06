using System.Collections.Generic;
using CompanyManager.Models;
using CompanyManager.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Services
{
    public class CompanyMangerService
    {
        /*
        private readonly ICompanyRepository _companyRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IQualificationRepository _qualificationRepository;

        public CompanyMangerService(
            ICompanyRepository companyRepository,
            IWorkerRepository workerRepository,
            IProjectRepository projectRepository,
            IQualificationRepository qualificationRepository)
        {
            _companyRepository = companyRepository;
            _workerRepository = workerRepository;
            _projectRepository = projectRepository;
            _qualificationRepository = qualificationRepository;
        }

        public void AssignWorkerToProject(int workerId, int projectId)
        {
            // Fetch the worker and the project from the database
            Worker worker = _workerRepository.GetById(workerId);
            Project project = _projectRepository.GetById(projectId);

            // Check if the worker is already assigned to this project
            var existingWorkerProject = _workerRepository.GetById(workerId);
            
            if(existingWorkerProject != null)
            {
                // Handle the situation (maybe show a message to the user)
                throw new InvalidOperationException("Worker is already assigned to this project.");
            }
            else
            {
                // Call a method on the Worker entity to assign it to the project
                worker.AddProject(project);

                // Update the worker and the project in the database
                _workerRepository.Update(worker);
                _projectRepository.Update(project);

                // Save changes to the database
                _workerRepository.SaveChanges();
                _projectRepository.SaveChanges();
            }
        }

        public void UnassignWorkerFromProject(int workerId, int projectId)
        {
            try
            {
                // Fetch the worker and the project from the database
                Worker worker = _workerRepository.GetById(workerId);
                Project project = _projectRepository.GetById(projectId);

                // Call a method on the Worker entity to unassign it from the project
                worker.RemoveProject(project);

                // Update the worker and the project in the database
                _workerRepository.Update(worker);
                _projectRepository.Update(project);

                // Save changes to the database
                _workerRepository.SaveChanges();
                _projectRepository.SaveChanges();
            }
            catch(DbUpdateException e){
                throw new DbUpdateException("Worker is not assigned to this project", e);
            }
        }

        public void startProject(int projectId){
            Project project = _projectRepository.GetById(projectId);
            project.SetStatus(ProjectStatus.ACTIVE);
            _projectRepository.Update(project);
            _projectRepository.SaveChanges();
        }
        public void startProject(String projectName)
        {
            Project project = _projectRepository.GetByName(projectName);
            project.SetStatus(ProjectStatus.ACTIVE);
            _projectRepository.Update(project);
            _projectRepository.SaveChanges();
        }

        public void finishProject(int projectId){
            Project project = _projectRepository.GetById(projectId);
            project.SetStatus(ProjectStatus.FINISHED);
            _projectRepository.Update(project);
            _projectRepository.SaveChanges();
        }
        public void finishProject(String projectName)
        {
            Project project = _projectRepository.GetByName(projectName);
            project.SetStatus(ProjectStatus.FINISHED);
            _projectRepository.Update(project);
            _projectRepository.SaveChanges();
        }
        public void createWorker(Worker worker){
            _workerRepository.Add(worker);
            _workerRepository.SaveChanges();
        }
        public void createProject(Project project){
            _projectRepository.Add(project);
            _projectRepository.SaveChanges();
        }
        public void createCompany(Company company){
            _companyRepository.Add(company);
            _companyRepository.SaveChanges();
        }
        public void createQualification(Qualification qualification){
            _qualificationRepository.Add(qualification);
            _qualificationRepository.SaveChanges();
        }
        */
    }

}