using Kiper.Condominio.Core.Commands;
using Kiper.Condominio.Core.Notifications;
using System.Threading.Tasks;

namespace Kiper.Condominio.Core.Interfaces
{
    public interface IMediatorBus
    {
        Task RaiseNotification(DomainNotification e);
        Task SendCommand<TEntity>(TEntity command) where TEntity : Command;
    }
}
