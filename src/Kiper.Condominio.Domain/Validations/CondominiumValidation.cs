using FluentValidation;
using Kiper.Condominio.Core.Validations;
using Kiper.Condominio.Domain.Entities;

namespace Kiper.Condominio.Domain.Validations
{
    public sealed class CondominiumValidation : Validation<Condominium>
    {
        public void ValidateCondominium()
        {
            RuleFor(condominium => condominium.Name)
                .NotEmpty().WithMessage("Nome é requerido");

            RuleFor(condominium => condominium.Address)
                .NotEmpty().WithMessage("Endereço é requerido");

            RuleFor(condominium => !condominium.Address.IsValid())
                .NotEqual(true).WithMessage("Endereço inválido");

            ValidateEntity();
        }
    }
}
