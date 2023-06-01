using CompanyManager.Models;
using CompanyManager.Repositories;
using CompanyManager.Services;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void StartProject(int projectId)
        {
            Project project = _projectRepository.GetById(projectId);
            project.SetStatus(ProjectStatus.ACTIVE);
            _projectRepository.Update(project);
            _projectRepository.SaveChanges();
        }

        public void StartProject(string projectName)
        {
            Project project = _projectRepository.GetByName(projectName);
            project.SetStatus(ProjectStatus.ACTIVE);
            _projectRepository.Update(project);
            _projectRepository.SaveChanges();
        }

        public void FinishProject(int projectId)
        {
            Project project = _projectRepository.GetById(projectId);
            project.SetStatus(ProjectStatus.FINISHED);
            _projectRepository.Update(project);
            _projectRepository.SaveChanges();
        }

        public void FinishProject(string projectName)
        {
            Project project = _projectRepository.GetByName(projectName);
            project.SetStatus(ProjectStatus.FINISHED);
            _projectRepository.Update(project);
            _projectRepository.SaveChanges();
        }

        public void CreateProject(Project project)
        {
            _projectRepository.Add(project);
            _projectRepository.SaveChanges();
        }
    }
}
