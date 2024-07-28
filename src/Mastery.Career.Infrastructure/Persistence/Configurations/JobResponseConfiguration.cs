using Mastery.Career.Domain.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mastery.Career.Infrastructure.Persistence.Configurations;

internal sealed class JobResponseConfiguration : IEntityTypeConfiguration<JobResponse>
{
    public void Configure(EntityTypeBuilder<JobResponse> builder)
    {
        builder.ToTable("job_responses");

        builder.HasKey(response => response.Id);

        builder.Property(response => response.Status)
            .HasConversion<string>()
            .HasMaxLength(32);
    }
}
