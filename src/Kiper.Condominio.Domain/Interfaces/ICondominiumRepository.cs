using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Domain.Entities;
using System;

namespace Kiper.Condominio.Domain.Interfaces
{
    public interface ICondominiumRepository : IRepository<Condominium>
    {
        Condominium FindByName(string name);
        bool HasApartments(Guid id);
    }
}
