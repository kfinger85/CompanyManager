using System.Collections.Generic;
using CompanyManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Services
{
    public interface IProjectService
    {
        ICollection<Project.ProjectDTO> FetchAllProjectDTOs();

        IEnumerable<Project> FetchAllProjects();

        Project GetProjectByName(string name);

        Project CreateProject(string name, ICollection<Qualification> qualifications, ProjectSize size ,Company company);

        bool IsWorkerAssignedToProject(Worker worker, Project project);

        (bool, string) AddWorker(Project project, Worker worker);

        (bool, string) RemoveWorker(Project project, Worker worker);

        (bool, string) AddQualification(Project project, Qualification qualification);

        ICollection<Qualification> GetMissingQualifications(Project project);

        bool WorkerIsHelpful(Project project);

        IActionResult UnassignWorkerFromProject(Project projectToUnassignFrom, Worker workerToUnassign);
    }
}
