using Microsoft.EntityFrameworkCore;
using odata.models;
using odata.repository.Configurations;

namespace odata.repository
{
    public sealed class EducationContext : DbContext
    {
        public EducationContext(DbContextOptions<EducationContext> options)
            : base(options)
        {
        }

        public DbSet<Degree> Degrees { get; set; }

        public DbSet<EducationClass> Classes { get; set; }

        public DbSet<Faculty> Faculty { get; set; }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DegreeConfiguration());
            modelBuilder.ApplyConfiguration(new EducationClassConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}