using Microsoft.AspNetCore.Mvc;
using CompanyManager.Repositories;
using System.Diagnostics;
using System.Text.Json;

namespace CompanyManager.Controllers
{
    public class QualificationController : Controller
    {
        private readonly IQualificationRepository _qualificationRepository;
        private readonly CompanyManagerContext _context;
        public QualificationController(IQualificationRepository qualificationRepository, CompanyManagerContext context)
        {
            _qualificationRepository = qualificationRepository;
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
                Debug.WriteLine(JsonSerializer.Serialize(qualifications)); // Log the JSON object

                return Ok(qualifications);
        }
        [HttpGet("qualifications/{name}")]
        public IActionResult Details(string name)
        {
            var qualification = _qualificationRepository.GetByName(name);
            if (qualification == null)
            {
                return NotFound();
            }
            return Ok(qualification);
        }

    }
}