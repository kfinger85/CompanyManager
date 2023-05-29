using System;
using System.Collections.Generic;
using System.Linq;
using CompanyManager.Models;
namespace CompanyManager.Repositories{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly CompanyManagerContext _context;

        public WorkerRepository(CompanyManagerContext context)
        {
            _context = context;
        }

        public Worker GetById(long id)
        {
            return _context.Workers.FirstOrDefault(w => w.Id == id);
        }


        public IEnumerable<Worker> GetAll()
        {
            return _context.Workers.ToList();
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