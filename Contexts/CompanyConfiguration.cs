using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CompanyManager.Contexts; 
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired();

        builder.HasMany(c => c.Workers)
            .WithOne(w => w.Company)
            .HasForeignKey(w => w.CompanyId);

        builder.HasMany(c => c.Projects)
            .WithOne(p => p.Company)
            .HasForeignKey(p => p.CompanyId);

        builder.HasMany(c => c.Qualifications)
            .WithMany()
            .UsingEntity<CompanyQualification>(
                j => j.HasOne(cq => cq.Qualification)
                    .WithMany()
                    .HasForeignKey(cq => cq.QualificationId),
                j => j.HasOne(cq => cq.Company)
                    .WithMany()
                    .HasForeignKey(cq => cq.CompanyId),
                j =>
                {
                    j.HasKey(cq => new { cq.CompanyId, cq.QualificationId });
                    j.ToTable("CompanyQualification");
                }
            );
    }
}
