using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;

public class CompanyManagerContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<WorkerProject> WorkerProjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;database=CompanyManger;user=root;password=root");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CompanyModelCreate(modelBuilder);
        modelBuilder.Entity<CompanyQualification>()
            .HasKey(cq => new { cq.CompanyId, cq.QualificationId });

        modelBuilder.Entity<ProjectQualification>()
            .HasKey(pq => new { pq.ProjectId, pq.QualificationId });

        modelBuilder.Entity<WorkerQualification>()
            .HasKey(wq => new { wq.WorkerId, wq.QualificationId });

        modelBuilder.Entity<WorkerProject>()
            .HasKey(wp => new { wp.WorkerId, wp.ProjectId });
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
            .WithOne(p => p.Workers)
            .HasForeignKey(p => p.WorkerId);

        modelBuilder.Entity<Worker>()
            .HasMany(w => w.Qualifications)
            .WithOne(q => q.Workers)
            .HasForeignKey(q => q.WorkerId);

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
