using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Models;

public partial class TestContext : DbContext
{
    static TestContext context;
    private TestContext()
    {
    }
    public static TestContext GetContext()
    {
        if (context == null) context = new TestContext();
        return context;
    }
    private TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auth> Auths { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Secret\\Laboratory-work\\3 Курс\\МДК 04.01\\Лабораторная работа 2\\ORM DatabaseFirst\\Лабораторная 2\\test.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auth>(entity =>
        {
            entity.ToTable("auth");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasMany(d => d.Books).WithMany(p => p.Auths)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthBook",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Auth>().WithMany()
                        .HasForeignKey("AuthId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("AuthId", "BooksId");
                        j.ToTable("auth_book");
                        j.IndexerProperty<int>("AuthId").HasColumnName("auth_id");
                        j.IndexerProperty<int>("BooksId").HasColumnName("books_id");
                    });
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("books");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
