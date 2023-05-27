using System.Collections.Generic;
using CompanyManager.Models;
using CompanyManager.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Services
{
    public class CompanyMangerService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IQualificationRepository _qualificationRepository;

        public CompanyMangerService(
            ICompanyRepository companyRepository,
            IWorkerRepository workerRepository,
            IProjectRepository projectRepository,
            IQualificationRepository qualificationRepository)
        {
            _companyRepository = companyRepository;
            _workerRepository = workerRepository;
            _projectRepository = projectRepository;
            _qualificationRepository = qualificationRepository;
        }

    }
}
