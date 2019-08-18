using Microsoft.EntityFrameworkCore;

namespace Kiper.Condominio.Infra.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void AddConfiguration<T>(this ModelBuilder modelBuilder, EntityTypeConfiguration<T> configuration) where T : class
        {
            configuration.Map(modelBuilder.Entity<T>());
        }
    }
}
