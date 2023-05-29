using System;
using System.Collections.Generic;
using CompanyManager.Models;

namespace CompanyManager.Repositories
{
    public interface IQualificationRepository
    {
        Qualification GetById(long id);
        Qualification GetByName(string name);
        IEnumerable<Qualification> GetAll();
        void Add(Qualification qualification);
        void Update(Qualification qualification);
        void Delete(Qualification qualification);
        void SaveChanges();
    }
}