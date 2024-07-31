using Mastery.Modules.Career.Domain.Companies;
using Mastery.Modules.Career.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mastery.Modules.Career.Infrastructure.Persistence.Configurations;

internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("companies");

        builder.HasKey(company => company.Id);

        builder.Property(company => company.Title)
            .HasMaxLength(255)
            .HasConversion(title => title.Value, value => CompanyTitle.From(value).Value);

        builder.OwnsOne(company => company.Category);

        builder.OwnsMany(company => company.Ratings, ratingBuilder =>
        {
            ratingBuilder.ToJson();
        });

        builder.Property(company => company.Note)
            .HasMaxLength(2048)
            .HasConversion(note => note!.Value, value => Note.New(value).Value);

        builder.Property<uint>("Version").IsRowVersion();
    }
}
