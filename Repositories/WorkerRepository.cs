using System;
using System.Collections.Generic;
using System.Linq;
using CompanyManager.Models;
using Microsoft.EntityFrameworkCore;
namespace CompanyManager.Repositories{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly CompanyManagerContext _context;

        public WorkerRepository(CompanyManagerContext context)
        {
            _context = context;
        }

        public Worker GetByName(string name)
        {
            return _context.Workers.FirstOrDefault(w => w.Name == name);
        }

        public Worker GetById(long id)
        {
            return _context.Workers.FirstOrDefault(w => w.Id == id);
        }


        public IEnumerable<Worker> GetAll()
        {
            return _context.Workers.ToList();
        }

        public IEnumerable<Worker> GetAllWithQualifications()
        {
            return _context.Workers.Include(w => w.Qualifications);
        }

        public void Add(Worker worker)
        {
            _context.Workers.Add(worker);
        }

        public void Update(Worker worker)
        {
            _context.Workers.Update(worker);
        }

        public void Delete(Worker worker)
        {
            _context.Workers.Remove(worker);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}