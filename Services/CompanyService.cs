using CompanyManager.Models;
using CompanyManager.Repositories;


namespace CompanyManager.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public void CreateCompany(Company company)
        {
            _companyRepository.Add(company);
            _companyRepository.SaveChanges();
        }
    }
}
