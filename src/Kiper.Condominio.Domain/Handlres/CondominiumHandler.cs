using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Core.Notifications;
using Kiper.Condominio.Core.Helpers.ValueObjects;
using Kiper.Condominio.Domain.Commands;
using Kiper.Condominio.Domain.Entities;
using Kiper.Condominio.Domain.Interfaces;
using Kiper.Condominio.Handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kiper.Condominio.Core.Helpers;

namespace Kiper.Condominio.Domain.Handlres
{
    public sealed class CondominiumHandler : CommandHandler,
        IRequestHandler<RegisterCondominium, bool>,
        IRequestHandler<UpdateCondominium, bool>,
        IRequestHandler<DeleteCondominium, bool>
    {
        private readonly ICondominiumRepository _condominiumRepository;

        public CondominiumHandler(IUnitOfWork uow, IMediatorBus mediator, INotificationHandler<DomainNotification> notifications, IUser user, ICondominiumRepository condominiumRepository) : base(uow, mediator, notifications, user)
        {
            _condominiumRepository = condominiumRepository;
        }

        public Task<bool> Handle(RegisterCondominium request, CancellationToken cancellationToken)
        {
            var found = _condominiumRepository.Any(c => c.Name == request.Name);

            if (found)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Condominium), "Condomínio já existe", NotificationType.Error));

                return Task.FromResult(false);
            }

            var address = new AddressInfo(request.AddressStreet, request.AddressNumber, request.AddressComplement, request.AddressNeighbourhood, request.AddressCity, request.AddressState, request.AddressZipCode);

            var condominium = Condominium.CondominiumFactory.NewCondominium(User.GetUserId(), request.Name, address);

            if (!IsAValidCondominium(condominium))
                return Task.FromResult(false);
            
            _condominiumRepository.Insert(condominium);

            if (Commit())
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Condominium), "Operação realizada com sucesso", NotificationType.Info));
                
                return Task.FromResult(true);
            }

            Mediator.RaiseNotification(new DomainNotification(nameof(Condominium), "Erro ao realizar a operação", NotificationType.Error));

            return Task.FromResult(false);
        }

        public Task<bool> Handle(UpdateCondominium request, CancellationToken cancellationToken)
        {
            var currentCondominium = _condominiumRepository.FindById(request.Id);

            if (currentCondominium == null)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Condominium), "Condomínio não existe", NotificationType.Error));

                return Task.FromResult(false);
            }

            var address = new AddressInfo(request.AddressStreet, request.AddressNumber, request.AddressComplement, request.AddressNeighbourhood, request.AddressCity, request.AddressState, request.AddressZipCode);

            currentCondominium.UpdateCondominium(User.GetUserId(), request.Name, address);

            if (!IsAValidCondominium(currentCondominium))
                return Task.FromResult(false);

            _condominiumRepository.Update(currentCondominium);

            if (Commit())
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Condominium), "Operação realizada com sucesso", NotificationType.Info));

                return Task.FromResult(true);
            }

            Mediator.RaiseNotification(new DomainNotification(nameof(Condominium), "Erro ao realizar a operação", NotificationType.Error));

            return Task.FromResult(false);
        }

        public Task<bool> Handle(DeleteCondominium request, CancellationToken cancellationToken)
        {
            var currentCondominium = _condominiumRepository.FindById(request.Id);

            if (currentCondominium == null)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Condominium), "Condomínio não existe", NotificationType.Error));

                return Task.FromResult(false);
            }

            var hasApartments = _condominiumRepository.HasApartments(currentCondominium.Id);

            if (hasApartments)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Condominium), "Condomínio com apartamentos não pode ser eliminado", NotificationType.Error));

                return Task.FromResult(false);
            }

            currentCondominium.Delete(User.GetUserId());
            _condominiumRepository.Update(currentCondominium);

            if (Commit())
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Condominium), "Operação realizada com sucesso", NotificationType.Info));

                return Task.FromResult(true);
            }

            Mediator.RaiseNotification(new DomainNotification(nameof(Condominium), "Erro ao realizar a operação", NotificationType.Error));

            return Task.FromResult(false);
        }

        private bool IsAValidCondominium(IValidation condominium)
        {
            if (condominium.IsValid())
                return true;

            Mediator.RaiseNotification(new DomainNotification(condominium.ValidationResult.Errors[0].PropertyName, condominium.ValidationResult.Errors[0].ErrorMessage, NotificationType.Error));
            return false;
        }
    }
}
