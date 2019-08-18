using Kiper.Condominio.Core.Entities;
using Kiper.Condominio.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Kiper.Condominio.Infra.Data.Mappings
{
    public abstract class BaseMap<T> : EntityTypeConfiguration<T> where T : Entity
    {
        public override void Map(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .IsRequired();

            builder.Property(entity => entity.Status)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(entity => entity.CreatedAt)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder.Property(entity => entity.ModifiedAt)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder.Property(entity => entity.CreatedBy)
                .IsRequired();

            builder.Property(entity => entity.ModifiedBy)
                .IsRequired();

            builder.Ignore(entity => entity.ValidationResult);
        }
    }
}
