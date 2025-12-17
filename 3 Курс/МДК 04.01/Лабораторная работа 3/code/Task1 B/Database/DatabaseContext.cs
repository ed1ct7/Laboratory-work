using System.IO;
using Microsoft.EntityFrameworkCore;
using ORM_Individual.Models.Entities;

namespace ORM_Individual.Models.Database;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext(){}
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
    static DatabaseContext context;
    public static DatabaseContext GetContext()
    {
        if (context == null) context = new DatabaseContext();
        return context;
    }
    public virtual DbSet<Component> Components { get; set; }
    public virtual DbSet<ComponentType> ComponentTypes { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Position> Positions { get; set; }
    public virtual DbSet<Service> Services { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        var dbPath = GetDatabasePath();
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
    private static string GetDatabasePath()
    {
        var baseDirectory = AppContext.BaseDirectory;
        var projectRoot = FindProjectRoot(baseDirectory);
        var dbDirectory = projectRoot ?? baseDirectory;
        var path = Path.Combine(dbDirectory, "Database.db");
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        return path;
    }
    private static string? FindProjectRoot(string startDirectory)
    {
        var directoryInfo = new DirectoryInfo(startDirectory);
        while (directoryInfo != null)
        {
            var projectFile = Path.Combine(directoryInfo.FullName, "ORM Individual.csproj");
            if (File.Exists(projectFile))
            {
                return directoryInfo.FullName;
            }
            directoryInfo = directoryInfo.Parent;
        }
        return null;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Component>(entity =>
        {
            entity.ToTable("Component");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ManufacturerCompany).HasColumnName("manufacturer_company");
            entity.Property(e => e.ManufacturerCountry).HasColumnName("manufacturer_country");
            entity.Property(e => e.Price)
                .HasColumnType("DECIMAL")
                .HasColumnName("price");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.Specifications).HasColumnName("specifications");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Warranty)
                .HasColumnType("INT")
                .HasColumnName("warranty");

            entity.HasOne(d => d.Type).WithMany(p => p.Components).HasForeignKey(d => d.TypeId);
        });
        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.ToTable("ComponentType");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
        });
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.Phone).HasColumnName("phone");
        });
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Age)
                .HasColumnType("INT")
                .HasColumnName("age");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasColumnType("BOOL")
                .HasColumnName("gender");
            entity.Property(e => e.PassportData).HasColumnName("passport_data");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.HasOne(d => d.Position).WithMany(p => p.Employees).HasForeignKey(d => d.PositionId);
        });
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.CompletionDate).HasColumnName("completion_date");
            entity.Property(e => e.Component1Id).HasColumnName("component1_id");
            entity.Property(e => e.Component2Id).HasColumnName("component2_id");
            entity.Property(e => e.Component3Id).HasColumnName("component3_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.IsCompleted)
                .HasColumnType("BOOL")
                .HasColumnName("is_completed");
            entity.Property(e => e.IsPaid)
                .HasColumnType("BOOL")
                .HasColumnName("is_paid");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.Prepayment)
                .HasColumnType("DECIMAL")
                .HasColumnName("prepayment");
            entity.Property(e => e.Service1Id).HasColumnName("service1_id");
            entity.Property(e => e.Service2Id).HasColumnName("service2_id");
            entity.Property(e => e.Service3Id).HasColumnName("service3_id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("DECIMAL")
                .HasColumnName("total_amount");
            entity.Property(e => e.TotalWarranty).HasColumnName("total_warranty");

            entity.HasOne(d => d.Component1).WithMany(p => p.OrderComponent1s).HasForeignKey(d => d.Component1Id);

            entity.HasOne(d => d.Component2).WithMany(p => p.OrderComponent2s).HasForeignKey(d => d.Component2Id);

            entity.HasOne(d => d.Component3).WithMany(p => p.OrderComponent3s).HasForeignKey(d => d.Component3Id);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.Service1).WithMany(p => p.OrderService1s).HasForeignKey(d => d.Service1Id);

            entity.HasOne(d => d.Service2).WithMany(p => p.OrderService2s).HasForeignKey(d => d.Service2Id);

            entity.HasOne(d => d.Service3).WithMany(p => p.OrderService3s).HasForeignKey(d => d.Service3Id);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Position");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Duties).HasColumnName("duties");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Requirements).HasColumnName("requirements");
            entity.Property(e => e.Salary)
                .HasColumnType("DECIMAL")
                .HasColumnName("salary");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("DECIMAL")
                .HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
