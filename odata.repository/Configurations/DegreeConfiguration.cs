using Microsoft.EntityFrameworkCore.Metadata.Builders;
using odata.models;

namespace odata.repository.Configurations
{
    internal sealed class DegreeConfiguration : BaseEntityConfiguration<Degree>
    {
        public override void Configure(EntityTypeBuilder<Degree> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);

            builder.Property(e => e.Description).HasMaxLength(2000);

            builder.HasIndex(e => e.Name).IsUnique(true);

            base.Configure(builder);
        }
    }
}