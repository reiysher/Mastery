﻿using Mastery.Modules.Identity.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mastery.Modules.Identity.Infrastructure.Persistence.Configurations;

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

        builder.OwnsOne(user => user.Email, emailBuilder =>
        {
            emailBuilder
                .Property(email => email.Value)
                .HasMaxLength(128)
                .HasColumnName("email_value");

            emailBuilder
                .Property(email => email.Confirmed)
                .HasColumnName("email_confirmed");
        });

        builder.OwnsOne(user => user.PhoneNumber, phoneNumberBuilder =>
        {
            phoneNumberBuilder
                .Property(phoneNumber => phoneNumber.CountryCode)
                .HasMaxLength(16)
                .HasColumnName("phone_number_country_code");
            
            phoneNumberBuilder
                .Property(phoneNumber => phoneNumber.Value)
                .HasMaxLength(32)
                .HasColumnName("phone_number_value");

            phoneNumberBuilder
                .Property(phoneNumber => phoneNumber.Confirmed)
                .HasColumnName("ephone_number_confirmed");
        });

        builder.HasMany(user => user.Roles)
            .WithMany()
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("user_roles");
            });

        builder.Property<uint>("Version").IsRowVersion();
    }
}
