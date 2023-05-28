using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;
using Pomelo.EntityFrameworkCore.MySql;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;



public class CompanyManagerContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<WorkerProject> WorkerProjects { get; set; }

        public CompanyManagerContext(DbContextOptions<CompanyManagerContext> options) : base(options)
        {
        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }


protected override void OnModelCreating(ModelBuilder modelBuilder)
{
      // Configure CompanyQualification entity
    modelBuilder.Entity<CompanyQualification>()
        .HasKey(cq => new { cq.CompanyId, cq.QualificationId });

    modelBuilder.Entity<CompanyQualification>()
        .ToTable("CompanyQualifications"); // Specify the table name explicitly

    modelBuilder.Entity<CompanyQualification>()
        .HasOne(cq => cq.Company)
        .WithMany()
        .HasForeignKey(cq => cq.CompanyId);

    modelBuilder.Entity<CompanyQualification>()
        .HasOne(cq => cq.Qualification)
        .WithMany()
        .HasForeignKey(cq => cq.QualificationId);

    // Configure ProjectQualification entity
    modelBuilder.Entity<ProjectQualification>()
        .HasKey(pq => new { pq.ProjectId, pq.QualificationId });

            modelBuilder.Entity<ProjectQualification>()
                .ToTable("ProjectQualification"); // Specify the table name explicitly

    modelBuilder.Entity<ProjectQualification>()
        .HasOne(pq => pq.Project)
        .WithMany()
        .HasForeignKey(pq => pq.ProjectId);

    modelBuilder.Entity<ProjectQualification>()
        .HasOne(pq => pq.Qualification)
        .WithMany()
        .HasForeignKey(pq => pq.QualificationId);

    // Configure WorkerQualification entity
    modelBuilder.Entity<WorkerQualification>()
        .HasKey(wq => new { wq.WorkerId, wq.QualificationId });

    modelBuilder.Entity<WorkerQualification>()
        .HasOne(wq => wq.Worker)
        .WithMany(w => w.WorkerQualifications)
        .HasForeignKey(wq => wq.WorkerId);

    modelBuilder.Entity<WorkerQualification>()
        .HasOne(wq => wq.Qualification)
        .WithMany(q => q.WorkerQualifications)
        .HasForeignKey(wq => wq.QualificationId);

    // Configure WorkerProject entity
    modelBuilder.Entity<WorkerProject>()
        .HasKey(wp => new { wp.WorkerId, wp.ProjectId });

            modelBuilder.Entity<WorkerProject>()
        .ToTable("WorkerProject"); // Specify the table name explicitly

    modelBuilder.Entity<WorkerProject>()
        .HasOne(wp => wp.Worker)
        .WithMany(w => w.WorkerProjects)
        .HasForeignKey(wp => wp.WorkerId);

    modelBuilder.Entity<WorkerProject>()
        .HasOne(wp => wp.Project)
        .WithMany(p => p.WorkerProjects)
        .HasForeignKey(wp => wp.ProjectId);
}

    private void WorkerModelCreate(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Worker>()
            .Property(w => w.Name)
            .IsRequired();

        modelBuilder.Entity<Worker>()
            .Property(w => w.Salary)
            .IsRequired();

        modelBuilder.Entity<Worker>()
            .Property(w => w.Username)
            .IsRequired();

        modelBuilder.Entity<Worker>()
            .Property(w => w.Password)
            .IsRequired();

modelBuilder.Entity<Worker>()
    .HasMany(w => w.Projects)
    .WithMany()
    .UsingEntity<WorkerProject>(
        j => j.HasOne(wp => wp.Project)
            .WithMany()
            .HasForeignKey(wp => wp.ProjectId),
        j => j.HasOne(wp => wp.Worker)
            .WithMany()
            .HasForeignKey(wp => wp.WorkerId),
        j =>
        {
            j.HasKey(wp => new { wp.WorkerId, wp.ProjectId });
            j.ToTable("WorkerProject");
        }
    );

            

modelBuilder.Entity<Worker>()
    .HasMany(w => w.Qualifications)
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
            j.ToTable("WorkerQualification");
        }
    );

        modelBuilder.Entity<Worker>()
            .HasMany(w => w.WorkerProjects)
            .WithOne(wp => wp.Worker)
            .HasForeignKey(wp => wp.WorkerId);

        modelBuilder.Entity<Worker>()
            .HasOne(w => w.Company)
            .WithMany(c => c.Workers)
            .HasForeignKey(w => w.CompanyId);
    }
}
