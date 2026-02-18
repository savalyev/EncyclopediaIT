namespace EncyclopediaIT.Migrations
{
    using EncyclopediaIT.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<EncyclopediaIT.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EncyclopediaIT.AppDbContext";
        }

        protected override void Seed(EncyclopediaIT.AppDbContext context)
        {
            var admin = new User
            {
                Username = "admin",
                Email = "admin@example.com",
                Password = HashPassword("admin123"),
                RegistrationDate = DateTime.Now,
                IsAdmin = true,
                IsBlocked = false
            };

            context.Users.Add(admin);
            context.SaveChanges();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
