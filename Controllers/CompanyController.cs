using Microsoft.AspNetCore.Mvc;
using CompanyManager.Repositories;
namespace CompanyManager.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        [HttpGet("companies")]
        public IActionResult Index()
        {
            return Ok(_companyRepository.GetAll());
        }
        [HttpGet("companies/{id}")]
        public IActionResult Details(int id)
        {
            var company = _companyRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }
    }

}