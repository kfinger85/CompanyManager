using System;
using System.Collections.Generic;
using System.Linq;
using CompanyManager.Models;
using CompanyManager.Contexts;


namespace CompanyManager.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly CompanyManagerContext _context;

        public ProjectRepository(CompanyManagerContext context)
        {
            _context = context;
        }

        public Project GetById(long id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.ToList();
        }

        public void Add(Project project)
        {
            _context.Projects.Add(project);
        }

        public void Update(Project project)
        {
            _context.Projects.Update(project);
        }

        public void Delete(Project project)
        {
            _context.Projects.Remove(project);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
