using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kiper.Condominio.WebApi.ViewModels
{
    public class ApartmentViewModel
    {
        [Required(ErrorMessage = "Numero é requerido")]
        [Range(1, 99, ErrorMessage = "Por favor insira um numero válido")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Bloco é requerido")]
        [StringLength(50, ErrorMessage = "O bloco excede o número de carateres", MinimumLength = 1)]
        public string Block { get; set; }

        [Required(ErrorMessage = "Andar é requerido")]
        [Range(1, 99, ErrorMessage = "Por favor insira um andar válido")]
        public int Roof { get; set; }
    }
}
