using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyManager.Contexts
{
    public class QualificationConfiguration : IEntityTypeConfiguration<Qualification>
    {
        public void Configure(EntityTypeBuilder<Qualification> builder)
        {
            builder.Property(q => q.Name)
                .IsRequired()
                .HasColumnName("name");

            builder.HasMany(q => q.Projects)
                .WithMany(p => p.Qualifications)
                .UsingEntity(j =>
                {
                    j.ToTable("project_qualification");
                });

            builder.HasMany(q => q.Workers)
                .WithMany(w => w.Qualifications)
                .UsingEntity(j =>
                {
                    j.ToTable("worker_qualification");
                });

            builder.HasMany(q => q.Companies)
                .WithMany(c => c.Qualifications)
                .UsingEntity(j =>
                {
                    j.ToTable("company_qualification");
                });
        }
    }
}
