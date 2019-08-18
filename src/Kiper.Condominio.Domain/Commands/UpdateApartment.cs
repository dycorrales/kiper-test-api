using Kiper.Condominio.Core.Commands;
using Kiper.Condominio.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kiper.Condominio.Domain.Commands
{
    public class UpdateApartment : Command, ICommand
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Block { get; set; }
        public int Roof { get; set; }
        public Guid CondominiumId { get; set; }
    }
}
