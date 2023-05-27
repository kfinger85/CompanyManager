using System.Collections.Generic;
using CompanyManager.Models;

namespace CompanyManager.Services
{
    public interface ICompanyService
    {
        Worker CreateWorker(string companyName, string name, HashSet<Qualification> qualifications, double salary);

        Qualification CreateQualification(string companyName, string description);

        Project CreateProject(string companyName, string name, HashSet<Qualification> qualifications, ProjectSize size);

        void StartProject(string companyName, string projectName);

        void FinishProject(string companyName, string projectName);

        void AssignWorkerToProject(string companyName, string workerName, string projectName);

        void UnassignWorkerFromProject(string companyName, string workerName, string projectName);

        void UnassignWorkerFromAllProjects(string companyName, string workerName);
    }
}
