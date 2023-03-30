namespace odata.models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}