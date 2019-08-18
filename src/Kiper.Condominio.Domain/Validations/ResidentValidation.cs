using System;
using FluentValidation;
using Kiper.Condominio.Core.Validations;
using Kiper.Condominio.Domain.Entities;

namespace Kiper.Condominio.Domain.Validations
{
    public sealed class ResidentValidation : Validation<Resident>
    {
        public void ValidateResident()
        {
            RuleFor(resident => resident.Name)
                .NotEmpty().WithMessage("Nome é requerido");

            RuleFor(resident => resident.Birthday)
                .InclusiveBetween(new DateTime(1900, 01, 01), DateTime.Today).WithMessage("Data de nascimento inválida");

            RuleFor(resident => resident.Contact)
                .NotNull().WithMessage("Contato é requerido");

            RuleFor(resident => resident.Contact.IsValid(resident.Contact))
                .Equal(true).WithMessage("Contato é inválido");

            RuleFor(resident => resident.Document)
                .NotNull().WithMessage("CPF é requerido");

            RuleFor(resident => resident.Document.Valid(resident.Document.Number))
                .Equal(true).WithMessage("CPF é inválido");

            RuleFor(resident => resident.ApartmentId)
                .NotEmpty().WithMessage("Apartamento é requerido");

            ValidateEntity();
        }
    }
}
