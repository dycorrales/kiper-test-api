using Kiper.Condominio.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kiper.Condominio.Domain.Commands
{
    public sealed class ResidentCommand : Command
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DocumentNumber { get; set; }
    }
}
