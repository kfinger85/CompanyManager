using Microsoft.AspNetCore.Mvc;
using CompanyManager.Repositories;
namespace CompanyManager.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        [HttpGet("projects")]
        public IActionResult Index()
        {
            return Ok(_projectRepository.GetAll());
        }
        [HttpGet("projects/{id}")]
        public IActionResult Details(int id)
        {
            var project = _projectRepository.GetById(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }
    }
}