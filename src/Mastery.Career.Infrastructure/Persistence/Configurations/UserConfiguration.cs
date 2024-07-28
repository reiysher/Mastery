using Mastery.Career.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mastery.Career.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder.OwnsOne(user => user.Name, nameBuilder =>
        {
            nameBuilder
                .Property(name => name.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");

            nameBuilder
                .Property(name => name.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
        });

        builder.Property(user => user.Email)
            .HasMaxLength(400)
            .HasConversion(email => email.Value, value => Email.Create(value));

        builder.Property<uint>("Version").IsRowVersion();
    }
}
