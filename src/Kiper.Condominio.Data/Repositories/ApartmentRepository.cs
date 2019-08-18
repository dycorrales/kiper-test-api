using Kiper.Condominio.Core.Helpers;
using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Domain.Entities;
using Kiper.Condominio.Domain.Interfaces;
using Kiper.Condominio.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Kiper.Condominio.Infra.Data.Repositories
{
    public sealed class ApartmentRepository : Repository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(ApplicationContext context, IUser user) : base(context, user)
		{
        }

        public bool HasResidents(Guid id)
        {
            return DbSet.Any(a => a.Id == id && a.Residents.Any(r => r.Status == Status.Active));
        }

        public Apartment GetApartmentWithResidents(Guid id)
        {
            return DbSet.Include(a => a.Residents).FirstOrDefault(a => a.Id == id);
        }

        public void RemoveResident(Resident resident)
        {
            Update(resident);
        }
    }
}
