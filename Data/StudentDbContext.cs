using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Models.Entity;

namespace StudentManagementAPI.Data;

public class StudentDbContext : DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Students");
            entity.HasKey(student => student.StudentId);

            entity.Property(student => student.StudentId)
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            entity.Property(student => student.Name)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(student => student.Email)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(student => student.Phone)
                .HasColumnType("nvarchar(15)")
                .HasMaxLength(15)
                .IsRequired();

            entity.Property(student => student.Age)
                .HasColumnType("int")
                .IsRequired();

            entity.Property(student => student.Course)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(student => student.Address)
                .HasColumnType("nvarchar(250)")
                .HasMaxLength(250)
                .IsRequired(false);
        });
    }
}
