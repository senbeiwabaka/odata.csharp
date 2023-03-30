namespace odata.models
{
    public abstract class Person : BaseModel
    {
        public string GivenName { get; set; } = default!;

        public string Surname { get; set; } = default!;

        public DateTime? Birthday { get; set; }
    }
}