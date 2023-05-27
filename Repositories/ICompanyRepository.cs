using System;
using CompanyManager.Models;

namespace CompanyManager.Repositories
{
    public interface ICompanyRepository
    {
        Company FindByName(string name);
        void Save(Company company);
    }
}
