using MediatR;
using System;

namespace Kiper.Condominio.Core.Commands
{
    public abstract class Command : IRequest<bool>
    {
    }
}
