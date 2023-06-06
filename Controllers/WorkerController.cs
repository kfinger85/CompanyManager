using Microsoft.AspNetCore.Mvc;
using CompanyManager.Services;
using CompanyManager.Models.DTO;
using CompanyManager.Models;
using System.Text.Json;
using System.Diagnostics;
namespace CompanyManager.Controllers
{
    public class WorkerController : Controller
    {
        private readonly WorkerService _workerService;
        private readonly CompanyService _companyService;
        private readonly CompanyManagerContext _context;

        public WorkerController(WorkerService workerService,CompanyService companyService, CompanyManagerContext context)
        {
           _context = context;
            _workerService = workerService;
            _companyService = companyService;
        }
        [HttpGet("workers")]
        public IActionResult Index()
        {
            try{
            var workers = _context.Workers
                .Select(w => new {
                    w.Name,
                    w.Salary,
                    w.Username,
                    w.Password,
                    CompanyName = w.Company.Name,
                    Qualifications = w.Qualifications.Select(q => q.Name),
                    Projects = _context.WorkerProject
                        .Where(wp => wp.WorkerId == w.Id)
                        .Select(wp => wp.Project.Name).ToList()
                })
                .ToList();
            
            return Ok(workers);
            }
            catch{
                return BadRequest();
            }
        }
        [HttpGet("workers/{name}")]
public IActionResult Details(string name)
{
    var worker = _context.Workers
        .Where(w => w.Name == name)
        .Select(w => new {
            w.Name,
            w.Salary,
            w.Username,
            w.Password,
            CompanyName = w.Company.Name,
            Qualifications = w.Qualifications.Select(q => q.Name),
            Projects = _context.WorkerProject
                .Where(wp => wp.WorkerId == w.Id)
                .Select(wp => wp.Project.Name)
                .ToList()
        })
        .FirstOrDefault();
    if (worker == null)
    {
        return NotFound("Worker not found.");
    }
    Debug.WriteLine(JsonSerializer.Serialize(worker)); // Log the JSON object
    return Ok(worker);
}

        [HttpPost("workers/{name}")]
        public IActionResult Create(String name, [FromBody] WorkerDTO workerDTO)
        {
            if (workerDTO == null)
            {
                return BadRequest("WorkerDTO is null");
            }

                string Name = name; 
                ICollection<Qualification> qualifications = workerDTO.Qualifications.Select(q => new Qualification(q)).ToList();
                double Salary = workerDTO.Salary; 
                string Username = workerDTO.Username;
                string Password = workerDTO.Password;
                string CompanyName = workerDTO.CompanyName;
                Company company =  _companyService.GetCompanyByName(CompanyName);
                if(company == null)
                {
                    return NotFound($"Company {CompanyName} does not exist");
                }

            _workerService.CreateWorker(Name, qualifications, Salary, company ,Username, Password);
            return Ok(); 
        }
    }

}