using Kiper.Condominio.Core.Entities;
using Kiper.Condominio.Core.Helpers.ValueObjects;
using Kiper.Condominio.Core.Interfaces;
using System;
using System.Collections.Generic;
using Kiper.Condominio.Domain.Validations;

namespace Kiper.Condominio.Domain.Entities
{
    public sealed class Condominium : Entity, IValidation
    {
        public string Name { get; private set; }
        public AddressInfo Address { get; private set; }
        public ICollection<Apartment> Apartments { get; protected set; }

        protected Condominium() { }

        private Condominium(Guid userId) : base(userId)
        {
            Apartments = new List<Apartment>();
        }

        public void UpdateCondominium(Guid userId, string name, AddressInfo address)
        {
            Name = name;

            Address.Update(address.Street, address.Number, address.Complement, address.Neighbourhood, address.City, address.State, address.ZipCode);

            Modify(userId);
        }

        public override bool IsValid()
        {
            var validation = new CondominiumValidation();
            validation.ValidateCondominium();
            ValidationResult = validation.Validate(this);

            return ValidationResult.IsValid;
        }

        public static class CondominiumFactory
        {
            public static Condominium NewCondominium(Guid userId, string name, AddressInfo address)
            {
                return new Condominium(userId)
                {
                    Name = name,
                    Address = address
                };
            }
        }
    }
}
