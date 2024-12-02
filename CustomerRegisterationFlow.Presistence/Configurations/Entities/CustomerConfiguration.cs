using CustomerRegisterationFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace HR.LeaveManagement.Persistence.Configurations.Entities
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
         
            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(p => p.Phone).IsUnique();
            builder.HasData(
                new Customer
                {
                    Id = 1,
                    ICNumber = 123456,
                    CreatedBy = "System",
                    DateCreated = DateTime.Now,
                    Email = "email1@email.com",
                    LastModifiedBy = "System",
                    LastModifiedDate = DateTime.Now,
                    Phone = "0101010101",
                    Name = "Ahmed 1",
                    PasswordHash="",
                    Salt=""
                },
                new Customer
                {
                    Id = 2,
                    ICNumber = 1234567,
                    CreatedBy = "System",
                    DateCreated = DateTime.Now,
                    Email = "email2@email.com",
                    LastModifiedBy = "System",
                    LastModifiedDate = DateTime.Now,
                    Phone = "01010101012",
                    Name = "Ahmed 2",
                    PasswordHash = "",
                    Salt = ""
                },
                    new Customer
                    {
                        Id = 3,
                        ICNumber = 12345678,
                        CreatedBy = "System",
                        DateCreated = DateTime.Now,
                        Email = "email3@email.com",
                        LastModifiedBy = "System",
                        LastModifiedDate = DateTime.Now,
                        Phone = "01010101013",
                        Name = "Ahmed 3",
                        PasswordHash = "",
                        Salt = ""
                    }
            );
        }
    }
}
