using FluentValidation.Results;
using Kiper.Condominio.Core.Helpers;
using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Core.Notifications;
using MediatR;

namespace Kiper.Condominio.Core.Handlers
{
    public abstract class Service
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorBus _mediator;
        private readonly DomainNotificationHandler _notifications;
        protected IUser User { get; }

        protected Service(IUnitOfWork uow, IMediatorBus mediator, INotificationHandler<DomainNotification> notifications, IUser user)
        {
            User = user;
            _uow = uow;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void ErrorNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _mediator.RaiseNotification(new DomainNotification(error.PropertyName, error.ErrorMessage, NotificationType.Error));
            }
        }

        protected bool Commit()
        {
            if (_notifications.HasNotifications(NotificationType.Error)) return false;
            var commandResponse = _uow.Commit();
            return commandResponse.Success;
        }
    }
}
