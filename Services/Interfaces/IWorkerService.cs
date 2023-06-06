using CompanyManager.Models;

namespace CompanyManager.Services
{
    public interface IWorkerService
    {
        void AssignWorkerToProject(int workerId, int projectId);
        void UnassignWorkerFromProject(int workerId, int projectId);
        void CreateWorker(Worker worker);
    }
}
