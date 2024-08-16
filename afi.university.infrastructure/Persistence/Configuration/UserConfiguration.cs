﻿using Microsoft.EntityFrameworkCore;
using afi.university.domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using afi.university.domain.Entities;

namespace afi.university.infrastructure.Persistence.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = new Guid("admin@gmail.com"),
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@gmail.com",
                    Password = "pass",
                    Role = domain.Common.Enums.UserRole.Admin
                },
                new User
                {
                    Id = new Guid("mickion@gmail.com"),
                    FirstName = "Mthokozisi",
                    LastName = "Mazibuko",
                    Email = "mickion@gmail.com",
                    Password = "pass",
                    Role = domain.Common.Enums.UserRole.Student
                },
                new User
                {
                    Id = new Guid("petunia@gmail.com"),
                    FirstName = "Petunia",
                    LastName = "Mazibuko",
                    Email = "petunia@gmail.com",
                    Password = "pass",
                    Role = domain.Common.Enums.UserRole.Lecture
                }
            );
        }
    }
}
