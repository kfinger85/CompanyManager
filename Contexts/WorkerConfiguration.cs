using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CompanyManager.Models;
namespace CompanyManager.Contexts; 


public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.Property(w => w.Name)
            .IsRequired();

        builder.Property(w => w.Salary)
            .IsRequired();

        builder.Property(w => w.Username)
            .IsRequired();

        builder.Property(w => w.Password)
            .IsRequired();

            builder.HasMany(w => w.Projects)
                .WithMany(p => p.Workers)
                .UsingEntity<WorkerProject>(
                    j => j.HasOne(wp => wp.Project)
                        .WithMany(p => p.WorkerProjects)
                        .HasForeignKey(wp => wp.ProjectId),
                    j => j.HasOne(wp => wp.Worker)
                        .WithMany(w => w.WorkerProjects)
                        .HasForeignKey(wp => wp.WorkerId),
                    j =>
                    {
                        j.HasKey(wp => new { wp.WorkerId, wp.ProjectId });
                        j.ToTable("WorkerProject");
                    }
                );

        builder.HasMany(w => w.Qualifications)
            .WithMany()
            .UsingEntity<WorkerQualification>(
                j => j.HasOne(wq => wq.Qualification)
                    .WithMany()
                    .HasForeignKey(wq => wq.QualificationId),
                j => j.HasOne(wq => wq.Worker)
                    .WithMany()
                    .HasForeignKey(wq => wq.WorkerId),
                j =>
                {
                    j.HasKey(wq => new { wq.WorkerId, wq.QualificationId });
                    j.ToTable("worker_qualification");
                }
            );

        builder.HasMany(w => w.WorkerProjects)
            .WithOne(wp => wp.Worker)
            .HasForeignKey(wp => wp.WorkerId);

        builder.HasOne(w => w.Company)
            .WithMany(c => c.Workers)
            .HasForeignKey(w => w.CompanyId);
    }
}
