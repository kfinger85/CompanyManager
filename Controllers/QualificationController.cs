using Microsoft.AspNetCore.Mvc;


using System.Diagnostics;
using System.Text.Json;

namespace CompanyManager.Controllers
{
    public class QualificationController : Controller
    {
        private readonly CompanyManagerContext _context;
        public QualificationController(CompanyManagerContext context)
        {
            _context = context;
        }
        [HttpGet("qualifications")]
        public IActionResult Index()
        {
            var qualifications = _context.Qualifications
                .Select(q => new {q.Name, 
                Workers = q.Workers.Select(w => w.Name).ToArray(), // Select the Name of each worker
                Projects = q.Projects.Select(p => p.Name).ToArray() // Select the Name of each project
                
                })  // Select the Name of each qualification
                .ToList();
                #if DEBUG
                Debug.WriteLine(JsonSerializer.Serialize(qualifications)); // Log the JSON object
                #endif
                return Ok(qualifications);
        }
        [HttpGet("qualifications/{name}")]
        public IActionResult Details(string name)
        {
            var qualification = _context.Qualifications.
            Select(q => new {q.Name, 
                Workers = q.Workers.Select(w => w.Name).ToArray().ToArray(), // Select the Name of each worker
                Projects = q.Projects.Select(p => p.Name).ToArray() // Select the Name of each project
                
                })  // Select the Name of each qualification
                .FirstOrDefault(q => q.Name == name);
            if (qualification == null)
            {
                return NotFound();
            }
            return Ok(qualification);
        }

    }
}