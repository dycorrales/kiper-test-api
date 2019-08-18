using System;
using System.Collections.Generic;
using System.Text;

namespace Kiper.Condominio.Core.Interfaces
{
    public interface ICommand
    {
        Guid Id { get; set; }
    }
}
