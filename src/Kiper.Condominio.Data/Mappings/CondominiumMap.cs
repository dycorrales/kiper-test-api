using Kiper.Condominio.Domain.Entities;
using Kiper.Condominio.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kiper.Condominio.Infrastructure.Data.Mappings
{
    public sealed class CondominiumMap : BaseMap<Condominium>
    {
        public override void Map(EntityTypeBuilder<Condominium> builder)
        {
            builder.ToTable("Condominium");
            
            builder.Property(condominium => condominium.Name)
                .HasColumnType("varchar(150)")
                .IsRequired();

			builder.OwnsOne(c => c.Address, address =>
			{
                address.Property(street => street.Street)
					.HasColumnName("Street")
					.HasColumnType("varchar(150)")
					.IsRequired();

                address.Property(number => number.Number)
                    .HasColumnName("Number")
                    .HasColumnType("int")
                    .IsRequired();

                address.Property(complement => complement.Complement)
                    .HasColumnName("Complement")
                    .HasColumnType("varchar(150)");

                address.Property(neighbourhood => neighbourhood.Neighbourhood)
                    .HasColumnName("Neighbourhood")
                    .HasColumnType("varchar(150)")
                    .IsRequired();

                address.Property(city => city.City)
                    .HasColumnName("City")
                    .HasColumnType("varchar(150)")
                    .IsRequired();

                address.Property(state => state.State)
                    .HasColumnName("State")
                    .HasColumnType("varchar(150)")
                    .IsRequired();

                address.Property(zipCode => zipCode.ZipCode)
                    .HasColumnName("ZipCode")
                    .HasColumnType("varchar(15)")
                    .IsRequired();
            });

            base.Map(builder);
        }
    }
}