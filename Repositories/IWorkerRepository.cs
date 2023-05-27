using System;
using System.Collections.Generic;
using CompanyManager.Models;

namespace CompanyManager.Repositories
{
    public interface IWorkerRepository
    {
        Worker GetById(long id);
        IEnumerable<Worker> GetAll();
        void Add(Worker worker);
        void Update(Worker worker);
        void Delete(Worker worker);
        void SaveChanges();
    }
}
