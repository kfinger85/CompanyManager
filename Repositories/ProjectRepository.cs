using System;
using System.Collections.Generic;
using System.Linq;
using CompanyManager.Models;
using Microsoft.EntityFrameworkCore;



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

        public Project GetByName(string name)
        {
            return _context.Projects.FirstOrDefault(p => p.Name == name);
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.ToList();
        }

        public IEnumerable<Project> GetAllWithQualifications()
        {
            return _context.Projects.Include(p => p.Qualifications);
        }


        public void Add(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
            
            if (project.Qualifications == null)
            {
                throw new ArgumentNullException(nameof(project.Qualifications), "The qualifications collection is null.");
            }
    
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
        public void StartProject(string projectName)
        {
            Project project = _context.Projects.FirstOrDefault(p => p.Name == projectName);
            if (project == null)
            {
                throw new InvalidOperationException("Project not found.");
            }
            _context.Projects.Update(project);
        }
        public void FinishProject(string projectName)
        {
            Project project = _context.Projects.FirstOrDefault(p => p.Name == projectName);
            if (project == null)
            {
                throw new InvalidOperationException("Project not found.");
            }
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

    }
}
