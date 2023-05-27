using System;
using System.Collections.Generic;
using CompanyManager.Models;

namespace CompanyManager.Repositories
{
    public interface IProjectRepository
    {
        Project GetById(long id);
        IEnumerable<Project> GetAll();
        void Add(Project project);
        void Update(Project project);
        void Delete(Project project);
        void SaveChanges();
    }
}