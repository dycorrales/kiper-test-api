using Kiper.Condominio.Core.Helpers;
using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Domain.Entities;
using Kiper.Condominio.Domain.Interfaces;
using Kiper.Condominio.Infra.Data.Contexts;
using System;
using System.Linq;

namespace Kiper.Condominio.Infra.Data.Repositories
{
    public sealed class CondominiumRepository : Repository<Condominium>, ICondominiumRepository
    {
		public CondominiumRepository(ApplicationContext context, IUser user) : base(context, user)
		{
		}

        public Condominium FindByName(string name)
        {
            return DbSet.FirstOrDefault(c => c.Name == name);
        }

        public bool HasApartments(Guid id)
        {
            return DbSet.Any(c => c.Id == id && c.Apartments.Any(a => a.Status == Status.Active));
        }
    }
}
