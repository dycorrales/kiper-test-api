using Kiper.Condominio.WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kiper.Condominio.WebApi.ViewModels
{
    public class RegisterApartmentViewModel : ApartmentViewModel
    {
        [Required(ErrorMessage = "Residente é requerido")]
        [MustHaveOneElementAttribute(ErrorMessage = "O partamento deve ter residentes")]

        public IEnumerable<ResidentViewModel> Residents { get; set; }
    }
}
