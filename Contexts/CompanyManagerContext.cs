#nullable disable

using CompanyManager.Models;

using MusicProduction.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using CompanyManager.Logging;

public class CompanyManagerContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<Worker> Workers { get; set; }

    public DbSet<WorkerProject> WorkerProject { get; set; }

    public DbSet<MissingQualification> MissingQualifications { get; set; }

    public DbSet<IdentityUser> Users { get; set; }

    // Music Production related entities

    // public DbSet<Artist> Artists { get; set; }
    // public DbSet<Stage> Stages { get; set; }
    // public DbSet<StageArtist> StageArtists { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderLineItem> OrderLineItems { get; set; }






    public CompanyManagerContext(DbContextOptions<CompanyManagerContext> options) : base(options)
         
    {

        
        this.Database.EnsureCreated();  // create the database
    }

    public override void Dispose()
    {
        Logger.LogInformation("CompanyManagerContext disposed.");
        base.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        Logger.LogInformation("CompanyManagerContext disposed asynchronously.");
        await base.DisposeAsync();
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

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        });
   
        
        modelBuilder.Entity<ApplicationUser>()
            .Property(u => u.AccessFailedCount)
            .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .HasDiscriminator<string>("UserType")
                .HasValue<ApplicationUser>("ApplicationUser")
                .HasValue<Worker>("Worker");

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Claims)
            .WithOne()
            .HasForeignKey(c => c.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Logins)
            .WithOne()
            .HasForeignKey(l => l.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

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

        // A Company has many Projects. Each Project is associated with one Company.
        modelBuilder.Entity<Company>()
            .HasMany(c => c.Projects)
            .WithOne(p => p.Company);

        // A Worker can work on many Projects and a Project can have many Workers working on it.
        // The relationship is managed through a joining table "WorkerProject".
            modelBuilder.Entity<WorkerProject>()
                .HasKey(wp => new { wp.WorkerId, wp.ProjectId });

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


    // For the relationship between ProductCategory and its SubCategories
    modelBuilder.Entity<ProductCategory>()
        .HasMany(p => p.SubCategories)
        .WithOne(c => c.ParentProductCategory)
        .HasForeignKey(c => c.ParentProductCategoryId);

    // For the relationship between Product and its Parent ProductCategory
    modelBuilder.Entity<Product>()
        .HasOne(p => p.ParentProductCategory)
        .WithMany(c => c.Products)
        .HasForeignKey(p => p.ParentProductCategoryId);

    // For the relationship between Product and its Sub ProductCategory
    modelBuilder.Entity<Product>()
        .HasOne(p => p.SubProductCategory)
        .WithMany(c => c.SubCategoryProducts)  // Use the new property
        .HasForeignKey(p => p.SubProductCategoryId);

    modelBuilder.Entity<Order>()
        .HasMany(o => o.OrderLineItems)
        .WithOne(oli => oli.Order)
        .HasForeignKey(oli => oli.OrderId);

    modelBuilder.Entity<OrderLineItem>()
        .HasOne(oli => oli.Product)
        .WithMany()  // There's no need for a collection of OrderLineItems in Product
        .HasForeignKey(oli => oli.ProductId);
            
        modelBuilder.Entity<ProductCategory>().HasData(
        new ProductCategory { ProductCategoryId = 1, Name = "guitars" },
        new ProductCategory { ProductCategoryId = 2, Name = "guitarsamps" },
        new ProductCategory { ProductCategoryId = 3, Name = "keyboards" },
        new ProductCategory { ProductCategoryId = 4, Name = "basscabs" },
        new ProductCategory { ProductCategoryId = 5, Name = "bassamps" },
        new ProductCategory { ProductCategoryId = 6, Name = "drums" },
        new ProductCategory { ProductCategoryId = 7, Name = "percussion" },
        new ProductCategory { ProductCategoryId = 8, Name = "drumhardware" , ParentProductCategoryId = 6 },
        new ProductCategory { ProductCategoryId = 9, Name = "snaredrum ", ParentProductCategoryId = 6    },
        new ProductCategory { ProductCategoryId = 10, Name = "kickdrum" , ParentProductCategoryId = 6    },
        new ProductCategory { ProductCategoryId = 11, Name = "tomdrum"  , ParentProductCategoryId = 6    },
        new ProductCategory { ProductCategoryId = 12, Name = "cymbals"   , ParentProductCategoryId = 6    }
    );
    }
}
