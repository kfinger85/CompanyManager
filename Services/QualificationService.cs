using CompanyManager.Models;
using CompanyManager.Repositories;
using System;

namespace CompanyManager.Services
{
    public class QualificationService : IQualificationService
    {
        private readonly IQualificationRepository _qualificationRepository;

        public QualificationService(IQualificationRepository qualificationRepository)
        {
            _qualificationRepository = qualificationRepository;
        }

        public void CreateQualification(Qualification qualification)
        {
            _qualificationRepository.Add(qualification);
            _qualificationRepository.SaveChanges();
        }
    }
}
