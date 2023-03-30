namespace odata.models
{
    public sealed class EducationClass : BaseModel
    {
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public DateTime FirstOffered { get; set; }

        public DateTime? LastOffered { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();

        public ICollection<Faculty> Faculty { get; set; } = new List<Faculty>();

        public ICollection<Degree> Degrees { get; set; } = new List<Degree>();
    }
}