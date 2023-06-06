using CompanyManager.Models;
using Microsoft.EntityFrameworkCore;

public class CompanyManagerContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<Worker> Workers { get; set; }

    public DbSet<WorkerProject> WorkerProject { get; set; }

    public DbSet<MissingQualification> MissingQualifications { get; set; }

    public CompanyManagerContext(DbContextOptions<CompanyManagerContext> options) : base(options)
    {

        this.Database.EnsureCreated();  // create the database
    }

    public CompanyManagerContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
    {
        // Enable logging with parameter values
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }
}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

            modelBuilder.Entity<Project>()
            .Property(p => p.Size)
            .HasConversion(
                v => v.ToString(), 
                v => (ProjectSize) Enum.Parse(typeof(ProjectSize), v));
            modelBuilder.Entity<Project>()
                .Property(p => p.Status)
                .HasConversion(
                    v => v.ToString(), 
                    v => (ProjectStatus) Enum.Parse(typeof(ProjectStatus), v));

        modelBuilder.Entity<Project>()
            .HasOne(p => p.Company)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.CompanyId);

        modelBuilder.Entity<Worker>()
            .HasOne(w => w.Company)
            .WithMany(c => c.Workers)
            .HasForeignKey(w => w.CompanyId);

        modelBuilder.Entity<MissingQualification>()
            .HasKey(mq => new { mq.ProjectId, mq.QualificationId });

        modelBuilder.Entity<MissingQualification>()
            .HasOne(mq => mq.Project)
            .WithMany(p => p.MissingQualifications)
            .HasForeignKey(mq => mq.ProjectId);

            modelBuilder.Entity<Project>()
                    .HasMany(p => p.Qualifications)
                    .WithMany(q => q.Projects)
                    .UsingEntity(j => j.ToTable("ProjectQualifications")); // This creates the joining table.

        
        // A Company has many Workers. Each Worker is associated with one Company. 
        modelBuilder.Entity<Company>()
            .HasMany(c => c.Workers)
            .WithOne(w => w.Company); 

        // A Company has many Qualifications and a Qualification can be associated with many Companies. 
        // The relationship is managed through a joining table "CompanyQualification".
        /*
        modelBuilder.Entity<Company>()
            .HasMany(c => c.Qualifications)
            .WithMany(q => q.Companies)
            .UsingEntity(j => j.ToTable("CompanyQualification"));
          */  
        // A Company has many Projects. Each Project is associated with one Company.
        modelBuilder.Entity<Company>()
            .HasMany(c => c.Projects)
            .WithOne(p => p.Company);

        // A Worker can work on many Projects and a Project can have many Workers working on it.
        // The relationship is managed through a joining table "WorkerProject".
            modelBuilder.Entity<WorkerProject>()
                .HasKey(wp => new { wp.WorkerId, wp.ProjectId });
/*
            modelBuilder.Entity<WorkerProject>()
                .HasOne(wp => wp.Worker)
                .WithMany(w => w.WorkerProjects)
                .HasForeignKey(wp => wp.WorkerId)
                .HasForeignKey(wp => wp.ProjectId);
                /*
            modelBuilder.Entity<WorkerProject>()
                .HasOne(wp => wp.Project)
                .WithMany(p => p.WorkerProjects)
                .HasForeignKey(wp => wp.ProjectId);
                */
        // A Worker can have many Qualifications and a Qualification can be possessed by many Workers.
        // The relationship is managed through a joining table "WorkerQualification".
        modelBuilder.Entity<Worker>()
            .HasMany(w => w.Qualifications)
            .WithMany(q => q.Workers)
            .UsingEntity(j => j.ToTable("WorkerQualification"));

        // A Worker is associated with one Company and a Company can have many Workers.
        // When a Worker is deleted, it is also removed from the list of Workers of its associated Company.
        modelBuilder.Entity<Worker>()
            .HasOne(w => w.Company)
            .WithMany(c => c.Workers) 
            .OnDelete(DeleteBehavior.Cascade);

        // A Project is associated with one Company and a Company can have many Projects.
        // When a Project is deleted, it is also removed from the list of Projects of its associated Company.
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Company)
            .WithMany(c => c.Projects)
            .OnDelete(DeleteBehavior.Cascade);

        // A Qualification can be associated with many Companies and a Company can require many Qualifications.
        // The relationship is managed through a joining table "CompanyQualification".
        /*
        modelBuilder.Entity<Qualification>()
            .HasMany(q => q.Companies)
            .WithMany(c => c.Qualifications)
            .UsingEntity(j => j.ToTable("CompanyQualification"));
            }
            */
    }
}
