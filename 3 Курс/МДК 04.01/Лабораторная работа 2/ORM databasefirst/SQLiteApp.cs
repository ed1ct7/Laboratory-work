using Microsoft.EntityFrameworkCore;
using Models;

namespace SQLiteApp
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Auth> Auths { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=test.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map entity classes to actual table names
            modelBuilder.Entity<Auth>().ToTable("auth");
            modelBuilder.Entity<Book>().ToTable("books");

            // Configure many-to-many relationship with correct table and column names
            modelBuilder.Entity<Auth>()
                .HasMany(a => a.Books)
                .WithMany(b => b.Auths)
                .UsingEntity<Dictionary<string, object>>(
                    "auth_book",
                    j => j.HasOne<Book>().WithMany().HasForeignKey("BookId"),
                    j => j.HasOne<Auth>().WithMany().HasForeignKey("AuthId"),
                    j =>
                    {
                        j.HasKey("AuthId", "BookId");
                    });
        }

        public static ApplicationContext GetContext()
        {
            return new ApplicationContext();
        }
    }
}