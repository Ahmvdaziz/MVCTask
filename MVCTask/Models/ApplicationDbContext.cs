using Microsoft.EntityFrameworkCore;
using MVCTask.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<CourseStudent> CourseStudents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseStudent>()
            .HasKey(cs => new { cs.CourseId, cs.StudentId });

        modelBuilder.Entity<CourseStudent>()
            .HasOne(cs => cs.Course)
            .WithMany(c => c.CourseStudents)
            .HasForeignKey(cs => cs.CourseId);

        modelBuilder.Entity<CourseStudent>()
            .HasOne(cs => cs.Student)
            .WithMany(s => s.CourseStudents)
            .HasForeignKey(cs => cs.StudentId);
    }

}
