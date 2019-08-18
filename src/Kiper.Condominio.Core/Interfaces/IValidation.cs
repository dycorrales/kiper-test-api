using FluentValidation.Results;

namespace Kiper.Condominio.Core.Interfaces
{
    public interface IValidation
    {
        bool IsValid();
        ValidationResult ValidationResult { get; set; }
    }
}
