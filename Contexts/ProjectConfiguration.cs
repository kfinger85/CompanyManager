using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CompanyManager.Contexts; 


public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired();

        builder.Property(p => p.Size)
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired();

        builder.HasOne(p => p.Company)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.CompanyId);

        builder.HasMany(p => p.Workers)
            .WithMany(w => w.Projects)
            .UsingEntity<WorkerProject>(
                j => j.HasOne(wp => wp.Worker)
                    .WithMany()
                    .HasForeignKey(wp => wp.WorkerId),
                j => j.HasOne(wp => wp.Project)
                    .WithMany()
                    .HasForeignKey(wp => wp.ProjectId),
                j =>
                {
                    j.HasKey(wp => new { wp.WorkerId, wp.ProjectId });
                    j.ToTable("WorkerProject");
                }
            );

        builder.HasMany(p => p.Qualifications)
            .WithMany(q => q.Projects)
            .UsingEntity<ProjectQualification>(
                j => j.HasOne(pq => pq.Qualification)
                    .WithMany()
                    .HasForeignKey(pq => pq.QualificationId),
                j => j.HasOne(pq => pq.Project)
                    .WithMany()
                    .HasForeignKey(pq => pq.ProjectId),
                j =>
                {
                    j.HasKey(pq => new { pq.ProjectId, pq.QualificationId });
                    j.ToTable("ProjectQualification");
                }
            );
    }
}
