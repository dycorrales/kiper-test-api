using Kiper.Condominio.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Kiper.Condominio.Infrastructure.Data.Mappings;
using FluentValidation.Results;
using Kiper.Condominio.Domain.Entities;

namespace Kiper.Condominio.Infra.Data.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Condominium> Condominiums { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Resident> Residents { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Ignore<ValidationFailure>();

            modelBuilder.AddConfiguration(new CondominiumMap());
            modelBuilder.AddConfiguration(new ApartmentMap());
            modelBuilder.AddConfiguration(new ResidentMap());
        }
    }
}
