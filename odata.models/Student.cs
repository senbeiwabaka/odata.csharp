namespace odata.models
{
    public sealed class Student : Person
    {
        public DateTime? GraduationDate { get; set; }

        public Guid? FacultyId { get; set; }

        public Faculty? Faculty { get; set; }

        public ICollection<Degree> Degrees { get; set; } = new List<Degree>();

        public ICollection<EducationClass> Classes { get; set; } = new List<EducationClass>();
    }
}