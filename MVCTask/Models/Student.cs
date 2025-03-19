namespace MVCTask.Models
{
    public class Student
    {
        public int StudentId { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();
    }
}
