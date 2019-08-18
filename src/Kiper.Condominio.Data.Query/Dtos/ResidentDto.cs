using System;
using System.Collections.Generic;
using System.Text;

namespace Kiper.Condominio.Data.Query.Dtos
{
    public partial class ResidentDto
    {
        public Guid Id { get; set; }
        public Guid ApartmentId { get; set; }
        public Guid CondominiumId { get; set; }
    }
}
