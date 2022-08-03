﻿using eStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.IdentityId)
                .HasMaxLength(30);
            builder.Property(c => c.FirstName)
                .HasMaxLength(120);
            builder.Property(c => c.LastName)
                .HasMaxLength(120);
            builder.Property(c => c.DateOfBirth)
                .HasColumnType("date");
            builder.Property(c => c.Email)
                .HasMaxLength(100);
            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(20);
            builder.Property(c => c.Country)
                .HasMaxLength(100);
            builder.Property(c => c.State)
                .HasMaxLength(2);
            builder.Property(c => c.City)
                .HasMaxLength(100);
            builder.Property(c => c.AddressLine1)
                .HasMaxLength(100);
            builder.Property(c => c.AddressLine2)
                .HasMaxLength(100);
            builder.Property(c => c.PostalCode)
                .HasMaxLength(10);
        }
    }
}