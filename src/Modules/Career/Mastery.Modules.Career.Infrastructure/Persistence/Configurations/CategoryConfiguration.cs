using Mastery.Modules.Career.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mastery.Modules.Career.Infrastructure.Persistence.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(category => category.Id);

        builder.Property(category => category.Value)
            .HasMaxLength(255);

        builder.Property(category => category.Description)
            .HasMaxLength(2048);

        builder.Property(category => category.Color)
            .HasMaxLength(255)
            .HasConversion(color => color.Value, value => Color.New(value));

        builder.Property<uint>("Version").IsRowVersion();
    }
}
