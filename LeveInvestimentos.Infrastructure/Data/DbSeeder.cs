using System;
using System.Linq;
using System.Threading.Tasks;
using LeveInvestimentos.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeveInvestimentos.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            await context.Database.MigrateAsync();

            if (!context.Users.Any())
            {
                var adminUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "ti@leveinvestimentos.com.br",
                    FullName = "Administrador TI",
                    BirthDate = new DateTime(1990, 1, 1),
                    Role = LeveInvestimentos.Domain.Enums.UserRole.Manager,
                    Address = new LeveInvestimentos.Domain.ValueObjects.Address("00000-000", "Rua TI", "123", "Centro", "São Paulo", "SP"),
                    MobilePhone = "000000000",
                    LandlinePhone = "000000000",
                    PhotoUrl = ""
                };

                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "teste123");

                context.Users.Add(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
