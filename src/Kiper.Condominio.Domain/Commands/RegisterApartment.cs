using Kiper.Condominio.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kiper.Condominio.Domain.Commands
{
    public sealed class RegisterApartment : Command
    {
        public int Number { get; set; }
        public string Block { get; set; }
        public int Roof { get; set; }
        public Guid CondominiumId { get; set; }
        public IEnumerable<ResidentCommand> Residents { get; set; }

    }
}
