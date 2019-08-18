using FluentValidation.Results;
using Kiper.Condominio.Core.Helpers;
using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Core.Notifications;
using MediatR;

namespace Kiper.Condominio.Handlers
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        protected readonly IMediatorBus Mediator;
        private readonly DomainNotificationHandler _notifications;
        protected IUser User { get; }

        protected CommandHandler(IUnitOfWork uow, IMediatorBus mediator, INotificationHandler<DomainNotification> notifications, IUser user)
        {
            User = user;
            _uow = uow;
            Mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void ErrorNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                Mediator.RaiseNotification(new DomainNotification(error.PropertyName, error.ErrorMessage, NotificationType.Error));
        }

        protected bool Commit()
        {
            if (_notifications.HasNotifications(NotificationType.Error)) return false;
            var commandResponse = _uow.Commit();
            return commandResponse.Success;
        }
    }
}
