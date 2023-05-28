using CompanyManager.Models;
using Microsoft.EntityFrameworkCore;

public class CompanyManagerContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<Worker> Workers { get; set; }

    public CompanyManagerContext(DbContextOptions<CompanyManagerContext> options) : base(options)
    {
        this.Database.EnsureDeleted();  // drop the database if it exists
        this.Database.EnsureCreated();  // create the database
    }

    public CompanyManagerContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Company
        modelBuilder.Entity<Company>()
            .HasMany(c => c.Workers)
            .WithOne(w => w.Company) // Add the navigation property here
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Company>()
            .HasMany(c => c.Qualifications)
            .WithMany(q => q.Companies)
            .UsingEntity<CompanyQualification>(
                cq => cq.HasOne(c => c.Qualification).WithMany(),
                cq => cq.HasOne(c => c.Company).WithMany()
            );
            
        modelBuilder.Entity<Company>()
            .HasMany(c => c.Projects)
            .WithOne(p => p.Company) // Add the navigation property here
            .OnDelete(DeleteBehavior.Cascade);


        // Configure Worker
        modelBuilder.Entity<Worker>()
            .HasMany(w => w.Projects)
            .WithMany(p => p.Workers);

        modelBuilder.Entity<Worker>()
            .HasMany(w => w.Qualifications)
            .WithMany(q => q.Workers);

        modelBuilder.Entity<Worker>()
              .HasOne(w => w.Company) // declares a relationship to Company
              .WithMany(c => c.Workers) // declares that Company has many Workers
              // .HasForeignKey(w => w.CompanyId) // explicitly sets the foreign key to CompanyId
              .OnDelete(DeleteBehavior.Cascade); // or whatever delete behavior you want

        modelBuilder.Entity<Project>()
            .HasOne(p => p.Company)
            .WithMany(c => c.Projects)
            // .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

            // Configure Qualification
        modelBuilder.Entity<Qualification>()
        .HasMany(q => q.Companies)
        .WithMany(c => c.Qualifications); 

        // modelBuilder.Entity<WorkerQualification>().HasKey(wq => new { wq.WorkerId, wq.QualificationId });
        modelBuilder.Entity<WorkerQualification>()
                .HasNoKey();
        modelBuilder.Entity<CompanyQualification>()
                .HasNoKey();
    }
}
