using Kiper.Condominio.Core.Commands;
using Kiper.Condominio.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kiper.Condominio.Domain.Commands
{
    public class DeleteApartment : Command, ICommand
    {
        public Guid Id { get; set; }
    }
}
