using System;
using System.Linq;
using CompanyManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Repositories
{
    public class CompanyRepository
    {
        private readonly CompanyManagerContext _context;

        public CompanyRepository(CompanyManagerContext context)
        {
            _context = context;
        }

        public Company GetById(int id)
        {
            return _context.Companies.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Company> GetAll()
        {
            return _context.Companies.ToList();
        }

        public void Add(Company company)
        {
            _context.Companies.Add(company);
        }

        public void Update(Company company)
        {
            _context.Companies.Update(company);
        }

        public void Delete(Company company)
        {
            _context.Companies.Remove(company);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

}
