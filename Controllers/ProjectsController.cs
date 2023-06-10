using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;


using CompanyManager.Models;
using CompanyManager.Services;
using CompanyManager.Logging;

using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Controllers
{
    public class ProjectController : Controller
    {
        private readonly CompanyManagerContext _context;
        private readonly CompanyService _companyService;
        private readonly WorkerService _workerService;
        private readonly ProjectService _projectService;

        public ProjectController(CompanyManagerContext context, CompanyService companyService, WorkerService workerService, ProjectService projectService)
        {
            _context = context;
            _companyService = companyService;
            _workerService = workerService;
            _projectService = projectService;
        }

        [HttpGet("projects")]
        public IActionResult Index()
        {
            var projects = _projectService.FetchAllProjectDTOs();

            if (projects == null)
            {
                return NotFound("Projects not found.");
            }
            
            Debug.WriteLine(JsonSerializer.Serialize(projects)); // Log the JSON object
            return Ok(projects);
        }
        [HttpGet("projects/{name}")]
        public IActionResult Details(string name)
        {
            var project = _context.Projects
                .Where(p => p.Name == name)
                .Select(p => new {
                    MissingQualifications = p.MissingQualifications.Select(mq => mq.Name),
                    Size = p.Size.ToString(),
                    p.Name,
                    Status = p.Status.ToString(),
                    Qualifications = p.Qualifications.Select(q => q.Name),
                    Workers = _context.WorkerProject
                        .Where(wp => wp.ProjectId == p.Id)
                        .Select(wp => wp.Worker.Name).ToList()
                })
                .FirstOrDefault();

            if (project == null)
            {
                return NotFound("Project not found.");
            }

            Debug.WriteLine(JsonSerializer.Serialize(project)); // Log the JSON object
            return Ok(project);
        }
        [HttpPut("projects/assign")]
        public IActionResult Assign([FromBody] AssignDTO assignDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try{
            var project = assignDTO.ProjectName;
            var worker = assignDTO.WorkerName;

            var projectToAssignTo = _projectService.GetProjectByName(project);
            var workerToAssign = _workerService.GetWorkerByName(worker);
            if (projectToAssignTo == null)
            {
                return NotFound("Project not found in database.");
            }
            if (workerToAssign == null)
            {
                return NotFound("Worker not found in database.");
            }

            _companyService.AssignWorkerToProject(workerToAssign, projectToAssignTo);


            return new OkResult();

            }catch(Exception e){
                return BadRequest(e.Message);
            }        

    }
        [HttpPut("projects/unassign")]
        public IActionResult UnassignWorkerFromProject([FromBody] AssignDTO assignDTO)
        {
            var project = assignDTO.ProjectName;
            var worker = assignDTO.WorkerName;

            var projectToUnassignFrom = _projectService.GetProjectByName(project);
            var workerToUnassign = _workerService.GetWorkerByName(worker);

            return _projectService.UnassignWorkerFromProject(projectToUnassignFrom, workerToUnassign);

        }
        [HttpPost("projects/start")]
        public IActionResult StartProject([FromBody] JsonElement jsonData)
        {
            try{
                string projectName = jsonData.GetProperty("projectName").GetString();
                return ChangeProjectStatus(projectName, ProjectStatus.ACTIVE, "Project is already started.");
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPost("projects/finish")]
        public IActionResult FinishProject([FromBody] JsonElement jsonData)
        {
            try{
            string projectName = jsonData.GetProperty("projectName").GetString();
            return ChangeProjectStatus(projectName, ProjectStatus.FINISHED, "Project is already finished.");
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        public IActionResult ChangeProjectStatus(string projectName, ProjectStatus newStatus, string alreadyInStatusMessage)
        {
            var projectToChange = _context.Projects
                .IncludeAllProjectRelations()
                .FirstOrDefault(p => p.Name == projectName);

            var (result, success) = ProjectStatusCheck(projectToChange);
            if (!success)
            {
                return result;
            }
            else
            {
                if (projectToChange.Status == newStatus)
                {
                    Logger.LogInformation($"{alreadyInStatusMessage} {projectToChange.Name}.");
                    return BadRequest(alreadyInStatusMessage);
                }

                projectToChange.Status = newStatus;
                _context.SaveChanges();

                Logger.LogInformation($"Changed status of project {projectToChange.Name} to {newStatus}.");

                return Ok();
            }
        }

        public (IActionResult, bool) ProjectStatusCheck(Project project)
        {
            if (project == null)
            {
                Logger.LogInformation($"Project {project?.Name} not found.");
                return (NotFound("Project not found."), false);
            }

            if (project.MissingQualifications.Any())
            {
                Logger.LogInformation($"Project {project.Name} cannot be started because it is missing qualifications.");
                return (BadRequest("Project cannot be started because it is missing qualifications."), false);
            }
            return (null, true);
        }


        public class AssignDTO
        {
            public string ProjectName { get; set; }
            public string WorkerName { get; set; }
        }

    }
        public static class QueryExtensions
        {
            public static IQueryable<Project> IncludeAllProjectRelations(this IQueryable<Project> query)
            {
                return query
                    .Include(p => p.WorkerProjects)
                        .ThenInclude(wp => wp.Worker)
                            .ThenInclude(w => w.Qualifications)
                    .Include(p => p.Company)
                    .Include(p => p.Qualifications)
                    .Include(p => p.MissingQualifications);
            }

        public static IQueryable<Worker> IncludeAllWorkerRelations(this IQueryable<Worker> query)
        {
            return query
                .Include(w => w.WorkerProjects)
                    .ThenInclude(wp => wp.Project)
                .Include(w => w.Qualifications)
                .Include(w => w.Company);
        }
    }
}