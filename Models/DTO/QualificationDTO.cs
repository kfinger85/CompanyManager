using System;

namespace CompanyManager.Models.DTO
{
    public class QualificationDTO
    {
        public string Description { get; set; }
        public string[] Workers { get; set; }

        public QualificationDTO() { }

        public QualificationDTO(string description, string[] workers)
        {
            Description = description;
            Workers = workers;
        }

        public QualificationDTO SetDescription(string description) // renamed method
        {
            Description = description;
            return this;
        }

        public QualificationDTO SetWorkers(string[] workers) // renamed method
        {
            Workers = workers;
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (!(obj is QualificationDTO))
                return false;
            QualificationDTO qualificationDTO = (QualificationDTO)obj;
            return Description == qualificationDTO.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Description);
        }

        public override string ToString()
        {
            return $"{{ description='{Description}', workers='{string.Join(",", Workers)}' }}";
        }
    }
}
