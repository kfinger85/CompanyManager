using CompanyManager.Models;

namespace CompanyManager.Repositories
{
    public interface ICompanyRepository
    {
        Company GetById(int id);
        IEnumerable<Company> GetAll();
        void Add(Company company);
        void Update(Company company);
        void Delete(Company company);
        void SaveChanges();
    }
}
