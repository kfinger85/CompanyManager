using Microsoft.AspNetCore.Mvc;
using CompanyManager.Repositories;
using CompanyManager.Models.DTO;
using CompanyManager.Models;
namespace CompanyManager.Controllers
{
    public class WorkerController : Controller
    {
        private readonly IWorkerRepository _workerRepository;
        public WorkerController(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }
        [HttpGet("workers")]
        public IActionResult Index()
        {
            return Ok(_workerRepository.GetAll());
        }
        [HttpGet("workers/{id}")]
        public IActionResult Details(long id)
        {
            var worker = _workerRepository.GetById(id);
            if (worker == null)
            {
                return NotFound();
            }
            return Ok(worker);
        }
        [HttpPost("workers/{name}")]
        public IActionResult Create(String name, [FromBody] WorkerDTO workerDTO)
        {
            if (workerDTO == null)
            {
                return BadRequest();
            }
            var worker = new Worker
            {
                Name = name,
                Qualifications = workerDTO.Qualifications.Select(q => new Qualification(q)).ToList(),
                Salary = workerDTO.Salary
            };
            _workerRepository.Add(worker);
            _workerRepository.SaveChanges();
            return Ok(); 
        }
    }

}