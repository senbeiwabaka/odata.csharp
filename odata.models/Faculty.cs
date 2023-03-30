using odata.models.Enums;

namespace odata.models
{
    public sealed class Faculty : Person
    {
        public FacultyRoles Role { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();

        public ICollection<Degree> Degrees { get; set; } = new List<Degree>();

        public ICollection<EducationClass> Classes { get; set; } = new List<EducationClass>();
    }
}