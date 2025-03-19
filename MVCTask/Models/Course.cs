namespace MVCTask.Models
{
    public class Course
    {
        public int CourseId { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int? InstructorId { get; set; } // Foreign Key
        public Instructor? Instructor { get; set; } = null!;

        public ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();
    }
}
