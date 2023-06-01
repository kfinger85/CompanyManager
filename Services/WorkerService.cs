using CompanyManager.Models;
using CompanyManager.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IProjectRepository _projectRepository;

        public WorkerService(IWorkerRepository workerRepository, IProjectRepository projectRepository)
        {
            _workerRepository = workerRepository;
            _projectRepository = projectRepository;
        }

        public void AssignWorkerToProject(int workerId, int projectId)
        {
            Worker worker = _workerRepository.GetById(workerId);
            Project project = _projectRepository.GetById(projectId);
            worker.AddProject(project);
            _workerRepository.Update(worker);
            _workerRepository.SaveChanges();
        }

        public void UnassignWorkerFromProject(int workerId, int projectId)
        {
            try
            {
                Worker worker = _workerRepository.GetById(workerId);
                Project project = _projectRepository.GetById(projectId);
                worker.RemoveProject(project);
                _workerRepository.Update(worker);
                _workerRepository.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Worker is not assigned to this project", e);
            }
        }

        public void CreateWorker(Worker worker)
        {
            _workerRepository.Add(worker);
            _workerRepository.SaveChanges();
        }
    }
}
