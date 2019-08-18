using FluentValidation;
using Kiper.Condominio.Core.Validations;
using Kiper.Condominio.Domain.Entities;

namespace Kiper.Condominio.Domain.Validations
{
    public sealed class ApartmentValidation : Validation<Apartment>
    {
        public void ValidateApartment()
        {
            RuleFor(apartment => apartment.Number)
                .GreaterThan(0).WithMessage("Número é requerido");

            RuleFor(apartment => apartment.Roof)
                .GreaterThan(0).WithMessage("Andar é requerido");

            RuleFor(apartment => apartment.Block)
                .NotEmpty().WithMessage("Bloco é requerido");

            RuleFor(apartment => apartment.CondominiumId)
                .NotEmpty().WithMessage("Condomínio é requerido");

            RuleFor(apartment => apartment.Residents)
                .NotEmpty().WithMessage("O apartamento deve possuir como mínimo um morador");

            ValidateEntity();
        }
    }
}
