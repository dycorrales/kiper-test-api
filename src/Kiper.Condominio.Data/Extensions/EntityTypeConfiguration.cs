using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kiper.Condominio.Infra.Data.Extensions
{
    public abstract class EntityTypeConfiguration<T> where T : class
    {
        public abstract void Map(EntityTypeBuilder<T> builder);
    }
}
