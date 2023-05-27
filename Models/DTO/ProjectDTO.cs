using System;

namespace CompanyManager.Models.DTO
{
    public class ProjectDTO
    {
        public string Name { get; set; }
        public ProjectSize Size { get; set; }
        public ProjectStatus Status { get; set; }
        public string[] Workers { get; set; }
        public string[] Qualifications { get; set; }
        public string[] MissingQualifications { get; set; }

        public ProjectDTO()
        {
        }

        public ProjectDTO(string name, ProjectSize size, ProjectStatus status, string[] workers, string[] qualifications, string[] missingQualifications)
        {
            Name = name;
            Size = size;
            Status = status;
            Workers = workers;
            Qualifications = qualifications;
            MissingQualifications = missingQualifications;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (!(obj is ProjectDTO))
                return false;
            ProjectDTO projectDTO = (ProjectDTO)obj;
            return Name == projectDTO.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public override string ToString()
        {
            return "{" +
                " name='" + Name + "'" +
                ", size='" + Size + "'" +
                ", status='" + Status + "'" +
                ", workers='" + Workers + "'" +
                ", qualifications='" + Qualifications + "'" +
                ", missingQualifications='" + MissingQualifications + "'" +
                "}";
        }
    }
}
