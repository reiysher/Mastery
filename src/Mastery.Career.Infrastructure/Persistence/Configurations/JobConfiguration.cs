using Mastery.Career.Domain.Companies;
using Mastery.Career.Domain.Jobs;
using Mastery.Career.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mastery.Career.Infrastructure.Persistence.Configurations;

internal sealed class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("jobs");

        builder.HasKey(job => job.Id);

        builder.Property(job => job.Note)
            .HasMaxLength(2048)
            .HasConversion(note => note!.Value, value => Note.New(value));

        builder.HasOne<Company>()
            .WithMany()
            .HasForeignKey(job => job.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(job => job.Responses)
            .WithOne()
            .HasForeignKey(response => response.JobId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property<uint>("Version").IsRowVersion();
    }
}
