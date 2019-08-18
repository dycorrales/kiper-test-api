using Kiper.Condominio.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kiper.Condominio.Domain.Commands
{
    public class RegisterCondominium : Command
    {
        public string Name { get; set; }
        public string AddressStreet { get; set; }
        public int AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string AddressNeighbourhood { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressZipCode { get; set; }
    }
}
