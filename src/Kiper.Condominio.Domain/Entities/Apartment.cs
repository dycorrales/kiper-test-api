using Kiper.Condominio.Core.Entities;
using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Domain.Validations;
using System;
using System.Collections.Generic;

namespace Kiper.Condominio.Domain.Entities
{
    public sealed class Apartment : Entity, IValidation
    {
        public int Number { get; private set; }
        public string Block { get; private set; }
        public int Roof { get; private set; }
        public Condominium Condominium { get; protected set; }
        public Guid CondominiumId { get; private set; }
        public ICollection<Resident> Residents { get; protected set; }

        protected Apartment() { }

        private Apartment(Guid userId) : base(userId)
        {
            Residents = new List<Resident>();
        }

        private Apartment(Guid id, Guid userId) : base(id, userId)
        {
            Residents = new List<Resident>();
        }

        public void UpdateApartment(Guid userId, int number, string block, int roof, Guid condominiumId)
        {
            Number = number;
            Block = block;
            Roof = roof;
            CondominiumId = condominiumId;

            Modify(userId);
        }

        public override bool IsValid()
        {
            var validation = new ApartmentValidation();
            validation.ValidateApartment();
            ValidationResult = validation.Validate(this);

            foreach(var resident in Residents)
                if(!resident.IsValid())
                    return resident.ValidationResult.IsValid;

            return ValidationResult.IsValid;
        }

        public void AddResident(Resident resident)
        {
            Residents.Add(resident);
        }

        public void RemoveResident(Resident resident)
        {
            Residents.Remove(resident);
        }
        
        public static class ApartmentFactory
        {
            public static Apartment NewApartment(Guid userId, int number, string block, int roof, Guid condominiumId, IList<Resident> residents)
            {
                return new Apartment(userId)
                {
                    Number = number,
                    Block = block,
                    Roof = roof,
                    CondominiumId = condominiumId,
                    Residents = residents
                };
            }

            public static Apartment NewApartment(Guid id, Guid userId, int number, string block, int roof, Guid condominiumId, IList<Resident> residents)
            {
                return new Apartment(id, userId)
                {
                    Number = number,
                    Block = block,
                    Roof = roof,
                    CondominiumId = condominiumId,
                    Residents = residents
                };
            }
        }
    }
}
