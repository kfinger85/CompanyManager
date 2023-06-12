using System.Collections.Generic;
using CompanyManager.Models;

namespace CompanyManager.Services
{
    public interface ICompanyService
    {
        Company GetCompanyByName(string name);
        Company CreateCompany(string name);
        void UpdateCompany(Company company);
        (bool success, string message) CreateQualification(string description);
        (bool success, string message) CreateProject(string name, ICollection<Qualification> qualifications, ProjectSize size);
        Worker CreateWorker(string name, ICollection<Qualification> qualifications, double salary, string username, string password);
        bool AssignWorkerToProject(Worker worker, Project project);
        bool UnassignWorkerFromProject(Worker worker, Project project);
    }
}
