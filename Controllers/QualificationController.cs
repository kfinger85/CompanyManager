using Microsoft.AspNetCore.Mvc;
using CompanyManager.Repositories;
namespace CompanyManager.Controllers
{
    public class QualificationController : Controller
    {
        private readonly IQualificationRepository _qualificationRepository;
        public QualificationController(IQualificationRepository qualificationRepository)
        {
            _qualificationRepository = qualificationRepository;
        }
        [HttpGet("qualifications")]
        public IActionResult Index()
        {
            return Ok(_qualificationRepository.GetAll());
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