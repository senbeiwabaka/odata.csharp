using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using odata.models;

namespace odata.repository.Configurations
{
    internal abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseModel
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}