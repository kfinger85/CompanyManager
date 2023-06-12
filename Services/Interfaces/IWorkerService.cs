#nullable enable
using System.Collections.Generic;
using CompanyManager.Models;

namespace CompanyManager.Services
{
    public interface IWorkerService
    {
        IEnumerable<Worker> GetWorkersForProject(Project project);

        Worker? GetWorkerById(int id);

        Worker? GetWorkerByName(string name);

        WorkerProject? GetWorkersProject(Worker worker, Project project);

        ICollection<Worker> GetWorkers();

        ICollection<Qualification> GetMatchingQualifications(Worker worker, Project project);

        bool IsWorkerAssignedToProject(Worker worker, Project project);

        Worker CreateWorker(string name, ICollection<Qualification> qualifications, double salary, Company company, string username, string password);

        bool AddProject(Worker worker, Project project, IEnumerable<Qualification> qualifications);

        (bool, string) RemoveProject(Worker worker, Project project);

        (bool, string) SetUsername(Worker worker, string username);

        (bool, string) SetPassword(Worker worker, string password);
    }
}
