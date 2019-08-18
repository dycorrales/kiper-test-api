using Kiper.Condominio.Core.Entities;
using Kiper.Condominio.Core.Helpers.ValueObjects;
using Kiper.Condominio.Core.Interfaces;
using System;
using Kiper.Condominio.Domain.Validations;


namespace Kiper.Condominio.Domain.Entities
{
    public sealed class Resident : Entity, IValidation
    {
        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }
        public ContactInfo Contact { get; private set; }
        public DocumentInfo Document { get; protected set; }
        public Guid ApartmentId { get; private set; }
        public Apartment Apartment { get; protected set; }

        protected Resident() { }

        private Resident(Guid userId) : base(userId)
        {
        }

        public void UpdateResident(Guid userId, string name, DateTime birthday, ContactInfo contact, DocumentInfo document, Guid apartmentId)
        {
            Name = name;
            Birthday = birthday;
            Contact = contact;
            Document = document;
            ApartmentId = apartmentId;

            Modify(userId);
        }

        public override bool IsValid()
        {
            var validation = new ResidentValidation();
            validation.ValidateResident();
            ValidationResult = validation.Validate(this);

            return ValidationResult.IsValid;
        }

        public static class ResidentFactory
        {
            public static Resident NewResident(Guid userId, string name, DateTime birthday, ContactInfo contact, DocumentInfo document, Guid apartmentId)
            {
                return new Resident(userId)
                {
                    Name = name,
                    Birthday = birthday,
                    Contact = contact,
                    Document = document,
                    ApartmentId = apartmentId
                };
            }
        }
    }
}
