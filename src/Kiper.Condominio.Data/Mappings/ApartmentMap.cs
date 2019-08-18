using Kiper.Condominio.Domain.Entities;
using Kiper.Condominio.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kiper.Condominio.Infrastructure.Data.Mappings
{
    public sealed class ApartmentMap : BaseMap<Apartment>
    {
        public override void Map(EntityTypeBuilder<Apartment> builder)
        {
            builder.ToTable("Apartment");
            
            builder.Property(apartment => apartment.Number)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(apartment => apartment.Block)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(apartment => apartment.Roof)
                .HasColumnType("int")
                .IsRequired();
            
            builder.HasOne(apartment => apartment.Condominium)
                .WithMany(condominium => condominium.Apartments)
                .HasForeignKey(apartment => apartment.CondominiumId)
                .IsRequired();

            base.Map(builder);
        }
    }
}