using System.Data.Entity;
using EncyclopediaIT.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EncyclopediaIT
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=TechEncyclopediaConnectionString")
        {
            // Инициализация БД
            Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Отключаем каскадное удаление по умолчанию
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Настройка связей
            modelBuilder.Entity<Technology>()
                .HasMany(t => t.Comments)
                .WithRequired(c => c.Technology)
                .WillCascadeOnDelete(true);
        }
    }
}