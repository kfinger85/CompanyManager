using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using CompanyManager.Models;
using CompanyManager.Services;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Controllers
{
    public class ProjectController : Controller
    {
        private readonly CompanyManagerContext _context;
        private readonly CompanyService _companyService;
        public ProjectController(CompanyManagerContext context, CompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }
        [HttpGet("projects")]
        public IActionResult Index()
        {
        var projects = _context.Projects
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
            .ToList();
            
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
        [HttpPost("projects/assign")]
        public IActionResult Assign([FromBody] AssignDTO assignDTO)
        {
            try{
            var project = assignDTO.ProjectName;
            var worker = assignDTO.WorkerName;

            var projectToAssign = _context.Projects
                .IncludeAllProjectRelations()
                .FirstOrDefault(p => p.Name == project);

            var workerToAssign = _context.Workers
                .IncludeAllWorkerRelations()
                .FirstOrDefault(w => w.Name == worker);

            if (projectToAssign == null)
            {
                return NotFound("Project not found.");
            }

            if (workerToAssign == null)
            {
                return NotFound("Worker not found.");
            }

            _companyService.AssignWorkerToProject(workerToAssign, projectToAssign);
            _context.SaveChanges();


            return Ok();
            }catch(Exception e){
                return BadRequest(e.Message);
            }

         

    }
        [HttpPost("projects/unassign")]
        public IActionResult UnassignWorkerFromProject([FromBody] AssignDTO assignDTO)
        {
            var project = assignDTO.ProjectName;
            var worker = assignDTO.WorkerName;

            var projectToUnassign = _context.Projects
                .IncludeAllProjectRelations()
                .FirstOrDefault(p => p.Name == project);

            var workerToUnassign = _context.Workers
                .IncludeAllWorkerRelations()
                .FirstOrDefault(w => w.Name == worker);

            if (projectToUnassign == null || workerToUnassign == null)
            {
                return NotFound("Project or worker not found.");
            }

            var workerProject = _context.WorkerProject
                .FirstOrDefault(wp => wp.WorkerId == projectToUnassign.Id && wp.ProjectId == workerToUnassign.Id);
            if (workerProject == null)
            {
               return NotFound("Worker not assigned to project.");
            }

            foreach (var qualification in workerToUnassign.Qualifications)
            {
                var projectQualification = projectToUnassign.Qualifications.FirstOrDefault(q => q.Id == qualification.Id);
                if (projectQualification != null)
                {
                    projectToUnassign.MissingQualifications.Add(new MissingQualification(projectToUnassign, qualification, qualification.Name));
                }
            }

            _context.WorkerProject.Remove(workerProject);
            projectToUnassign.WorkerProjects.Remove(workerProject);
            workerToUnassign.WorkerProjects.Remove(workerProject);
            _context.SaveChanges();


            return Ok();
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