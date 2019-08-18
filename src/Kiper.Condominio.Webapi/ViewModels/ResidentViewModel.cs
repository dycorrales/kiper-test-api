using Kiper.Condominio.WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kiper.Condominio.WebApi.ViewModels
{
    public class ResidentViewModel
    {
        [Required(ErrorMessage = "Nome é requerido")]
        [StringLength(250, ErrorMessage = "O nome excede o número de carateres", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Data nascimento é requerido")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ValidDate(ErrorMessage = "Insira uma data válida")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Telefone é requerido")]
        [StringLength(25, ErrorMessage = "O telefone excede o número de carateres", MinimumLength = 9)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email é requerido")]
        [StringLength(150, ErrorMessage = "O email excede o número de carateres", MinimumLength = 1)]
        public string Email { get; set; }

        [Required(ErrorMessage = "CPF é requerido")]
        [StringLength(11, ErrorMessage = "O cpf excede o número de carateres", MinimumLength = 11)]
        public string DocumentNumber { get; set; }
    }
}
