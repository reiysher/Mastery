using Mastery.Career.Domain.Companies;
using Mastery.Career.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mastery.Career.Infrastructure.Persistence.Configurations;

internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("companies");

        builder.HasKey(company => company.Id);

        builder.Property(company => company.Title)
            .HasMaxLength(255)
            .HasConversion(title => title.Value, value => CompanyTitle.From(value));

        builder.OwnsOne(company => company.Category);

        builder.OwnsMany(company => company.Ratings, ratingBuilder =>
        {
            ratingBuilder.ToJson();
        });

        builder.Property(company => company.Note)
            .HasMaxLength(2048)
            .HasConversion(note => note!.Value, value => Note.New(value));

        builder.Property<uint>("Version").IsRowVersion();
    }
}
