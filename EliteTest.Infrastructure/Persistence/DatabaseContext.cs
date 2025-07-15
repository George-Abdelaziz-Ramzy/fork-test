using EliteTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EliteTest.Infrastructure.Persistence;
public class DatabaseContext: DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Employee
        modelBuilder.Entity<Employee>()
            .HasKey(e => e.Id);

       modelBuilder.Entity<Employee>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Employee>()
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Employee>()
            .Property(e => e.HireDate)
            .IsRequired()
            .HasMaxLength(15);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region Department
        modelBuilder.Entity<Department>()
            .HasKey(d => d.Id);

        modelBuilder.Entity<Department>()
            .Property(d => d.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Department>()
            .Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Department>()
            .HasMany(d => d.Employees)
            .WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region EmployeeHistoryLog
        modelBuilder.Entity<EmployeeHistoryLog>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<EmployeeHistoryLog>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<EmployeeHistoryLog>()
            .HasOne(e => e.Employee)
            .WithMany(e => e.EmployeeHistoryLogs)
            .HasForeignKey(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

    }
}