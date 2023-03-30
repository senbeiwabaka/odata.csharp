using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using odata.models;

namespace odata.repository
{
    public static class EdmService
    {
        public static IEdmModel GetEdmModel()
        {
            var modelBuilder = new ODataConventionModelBuilder();

            modelBuilder.EntitySet<Degree>(typeof(Degree).Name);
            modelBuilder.EntitySet<EducationClass>(typeof(EducationClass).Name);

            var built = modelBuilder.GetEdmModel();

            return built;
        }
    }
}