
using System;
using System.Collections.Generic;
using System.Linq;
using CompanyManager.Models;
using CompanyManager.Contexts;

namespace CompanyManager.Repositories{
    public class QualificationRepository : IQualificationRepository
    {
        private readonly CompanyManagerContext _context;

        public QualificationRepository(CompanyManagerContext context)
        {
            _context = context;
        }

        public Qualification GetById(long id)
        {
            return _context.Qualifications.FirstOrDefault(q => q.Id == id);
        }

        public IEnumerable<Qualification> GetAll()
        {
            return _context.Qualifications.ToList();
        }

        public void Add(Qualification qualification)
        {
            _context.Qualifications.Add(qualification);
        }

        public void Update(Qualification qualification)
        {
            _context.Qualifications.Update(qualification);
        }

        public void Delete(Qualification qualification)
        {
            _context.Qualifications.Remove(qualification);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
