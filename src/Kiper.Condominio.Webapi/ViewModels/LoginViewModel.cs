using System.ComponentModel.DataAnnotations;

namespace Kiper.Condominio.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email é requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é requerida")]
        public string Password { get; set; }
    }
}
