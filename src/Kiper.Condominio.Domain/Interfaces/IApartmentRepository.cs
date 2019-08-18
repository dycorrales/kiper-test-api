using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Domain.Entities;
using System;

namespace Kiper.Condominio.Domain.Interfaces
{
    public interface IApartmentRepository : IRepository<Apartment>
    {
        bool HasResidents(Guid id);
        Apartment GetApartmentWithResidents(Guid id);
        void RemoveResident(Resident resident);
    }
}
