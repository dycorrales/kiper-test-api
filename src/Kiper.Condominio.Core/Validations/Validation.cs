using Kiper.Condominio.Core.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace Kiper.Condominio.Core.Validations
{
    public abstract class Validation<T> : AbstractValidator<T> where T : Entity
    {
        public void ValidateEntity()
        {
            RuleFor(entity => entity.Id)
                .NotEmpty().WithMessage("Identificador inválido");

            RuleFor(entity => entity.Status)
                .IsInEnum().WithMessage("Estatus inválido");

            RuleFor(entity => entity.CreatedAt.Date)
                .NotEmpty().WithMessage("Data de criação inválida");

            RuleFor(entity => entity.CreatedBy)
                .NotEmpty().WithMessage("Identificador inválido");

            RuleFor(entity => entity.ModifiedBy)
                .NotEmpty().WithMessage("Identificador inválido");

            RuleFor(entity => entity.ModifiedAt.Date)
                .NotEmpty().WithMessage("Data de modificação inválida");
        }

        public ValidationResult ValidationResult { get; protected set; }
    }
}
