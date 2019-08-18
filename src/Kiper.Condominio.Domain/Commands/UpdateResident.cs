using Kiper.Condominio.Core.Commands;
using Kiper.Condominio.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kiper.Condominio.Domain.Commands
{
    public class UpdateResident : Command, ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DocumentNumber { get; set; }
        public Guid ApartmentId { get; set; }
    }
}
