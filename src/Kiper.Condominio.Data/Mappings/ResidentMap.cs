using Kiper.Condominio.Domain.Entities;
using Kiper.Condominio.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kiper.Condominio.Infrastructure.Data.Mappings
{
    public sealed class ResidentMap : BaseMap<Resident>
    {
        public override void Map(EntityTypeBuilder<Resident> builder)
        {
            builder.ToTable("Resident");
            
            builder.Property(resident => resident.Name)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(resident => resident.Birthday)
                .IsRequired();

            builder.OwnsOne(r => r.Contact, contact =>
            {
                contact.Property(phoneNumber => phoneNumber.PhoneNumber)
                    .HasColumnName("PhoneNumber")
                    .HasColumnType("varchar(25)")
                    .IsRequired();

                contact.Property(email => email.Email)
                    .HasColumnName("Email")
                    .HasColumnType("varchar(150)")
                    .IsRequired();
            });

            builder.OwnsOne(r => r.Document, document =>
            {
                document.Property(cpf => cpf.Number)
                    .HasColumnName("Document")
                    .HasColumnType("varchar(11)")
                    .IsRequired();
            });

            builder.HasOne(resident => resident.Apartment)
                .WithMany(apartment => apartment.Residents)
                .HasForeignKey(resident => resident.ApartmentId)
                .IsRequired();

            base.Map(builder);
        }
    }
}