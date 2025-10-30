using Microsoft.EntityFrameworkCore;
using Models;

namespace SQLiteApp
{
    public class ApplicationContext : DbContext
    {
        //public DbSet<Auths> Auths { get; set; }
        //public DbSet<Books> Books { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=test.db");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Map entity classes to actual table names
        //    modelBuilder.Entity<Auths>().ToTable("auth");
        //    modelBuilder.Entity<Books>().ToTable("books");

        //    // Configure many-to-many relationship with correct table and column names
        //    modelBuilder.Entity<Auths>()
        //        .HasMany(a => a.Books)
        //        .WithMany(b => b.Auths)
        //        .UsingEntity<Dictionary<string, object>>(
        //            "auth_book",
        //            j => j.HasOne<Books>().WithMany().HasForeignKey("BookId"),
        //            j => j.HasOne<Auths>().WithMany().HasForeignKey("AuthId"),
        //            j =>
        //            {
        //                j.HasKey("AuthId", "BookId");
        //            });
        //}

        //public static ApplicationContext GetContext()
        //{
        //    return new ApplicationContext();
        //}
        internal static object GetContext()
        {
            throw new NotImplementedException();
        }
    }
}