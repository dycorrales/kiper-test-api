using System.Threading.Tasks;
using Kiper.Condominio.Core.Commands;
using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Core.Notifications;
using MediatR;

namespace Kiper.Condominio.Domain.Bus
{
    public class MediatorBus : IMediatorBus
    {
        private readonly IMediator _mediator;

        public MediatorBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task RaiseNotification(DomainNotification notification)
        {
            await _mediator.Publish(notification);
        }

        public async Task SendCommand<TEntity>(TEntity command) where TEntity : Command
        {
            await _mediator.Send(command);
        }
    }
}
