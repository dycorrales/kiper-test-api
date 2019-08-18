using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kiper.Condominio.WebApi.ViewModels
{
    public class CondominiumViewModel
    {
        [Required(ErrorMessage = "Nome é requerido")]
        [StringLength(150, ErrorMessage = "O nome excede o número de carateres", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Endereço é requerido")]
        public AddressViewModel Address { get; set; }
    }

    public class AddressViewModel
    {
        [Required(ErrorMessage = "Rua é requerido")]
        [StringLength(150, ErrorMessage = "A rua excede o número de carateres", MinimumLength = 1)]
        public string Street { get; set; }

        [Required(ErrorMessage = "Numero é requerido")]
        public int Number { get; set; }

        [StringLength(150, ErrorMessage = "O complemento excede o número de carateres", MinimumLength = 1)]
        public string Complement { get; set; }

        [Required(ErrorMessage = "Bairro é requerido")]
        [StringLength(150, ErrorMessage = "O bairro excede o número de carateres", MinimumLength = 1)]
        public string Neighbourhood { get; set; }

        [Required(ErrorMessage = "Cidade é requerido")]
        [StringLength(150, ErrorMessage = "A cidade excede o número de carateres", MinimumLength = 1)]
        public string City { get; set; }

        [Required(ErrorMessage = "Estado é requerido")]
        [StringLength(150, ErrorMessage = "O estado excede o número de carateres", MinimumLength = 1)]
        public string State { get; set; }

        [Required(ErrorMessage = "CEP é requerido")]
        [StringLength(15, ErrorMessage = "O CEP excede o número de carateres", MinimumLength = 1)]
        public string ZipCode { get; set; }
    }

}