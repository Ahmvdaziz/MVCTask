namespace MVCTask.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public List<Course> Courses { get; set; } = new();
    }

}
