using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kiper.Condominio.Data.Query.Dtos
{
    public class CondominiumDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ApartmentsNumber { get; set; }
    }
}
