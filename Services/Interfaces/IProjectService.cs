using CompanyManager.Models;

namespace CompanyManager.Services
{
    public interface IProjectService
    {
        void StartProject(int projectId);
        void StartProject(string projectName);
        void FinishProject(int projectId);
        void FinishProject(string projectName);
        void CreateProject(Project project);
    }
}
