#nullable enable

using System.Collections.Generic;
using CompanyManager.Models;

namespace CompanyManager.Services
{
public interface IQualificationService
{
    Qualification GetByName(string name);
    ICollection<Qualification> GetQualifications();
    Qualification CreateQualification(string description); // Updated method signature
    bool AddWorker(Qualification qualification, Worker worker);
    bool RemoveWorker(Qualification qualification, Worker worker);
}

}
