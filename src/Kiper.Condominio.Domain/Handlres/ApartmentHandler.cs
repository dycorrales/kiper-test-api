using Kiper.Condominio.Core.Helpers;
using Kiper.Condominio.Core.Helpers.ValueObjects;
using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Core.Notifications;
using Kiper.Condominio.Domain.Commands;
using Kiper.Condominio.Domain.Entities;
using Kiper.Condominio.Domain.Interfaces;
using Kiper.Condominio.Handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kiper.Condominio.Domain.Handlres
{
    public sealed class ApartmentHandler : CommandHandler,
        IRequestHandler<RegisterApartment, bool>,
        IRequestHandler<UpdateApartment, bool>,
        IRequestHandler<DeleteApartment, bool>,
        IRequestHandler<RegisterResident, bool>,
        IRequestHandler<UpdateResident, bool>,
        IRequestHandler<DeleteResident, bool>
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentHandler(IUnitOfWork uow, IMediatorBus mediator, INotificationHandler<DomainNotification> notifications, IUser user, IApartmentRepository apartmentRepository) : base(uow, mediator, notifications, user)
        {
            _apartmentRepository = apartmentRepository;
        }

        public Task<bool> Handle(RegisterApartment request, CancellationToken cancellationToken)
        {
            var found = _apartmentRepository.Any(c => c.Number == request.Number && c.Roof == request.Roof && c.Block == request.Block && c.CondominiumId == request.CondominiumId);

            if (found)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Apartamento já existe neste condomínio", NotificationType.Error));

                return Task.FromResult(false);
            }

            var residents = new List<Resident>();
            
            var apartmentId = Guid.NewGuid();

            foreach (var residentCommand in request.Residents)
            {
                var contact = new ContactInfo(residentCommand.PhoneNumber, residentCommand.Email);
                var document = new DocumentInfo(residentCommand.DocumentNumber);

                var newResident = Resident.ResidentFactory.NewResident(User.GetUserId(), residentCommand.Name, residentCommand.Birthday, contact, document, apartmentId);

                if (!newResident.IsValid())
                {
                    Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), $"Informação do residente {newResident.Name} inválida", NotificationType.Error));

                    return Task.FromResult(false);
                }

                residents.Add(newResident);
            }

            var apartment = Apartment.ApartmentFactory.NewApartment(apartmentId, User.GetUserId(), request.Number, request.Block, request.Roof, request.CondominiumId, residents);

            if (!IsAValidApartment(apartment))
                return Task.FromResult(false);

            _apartmentRepository.Insert(apartment);

            if (Commit())
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Operação realizada com sucesso", NotificationType.Info));

                return Task.FromResult(true);
            }

            Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Erro ao realizar a operação", NotificationType.Error));

            return Task.FromResult(false);
        }

        public Task<bool> Handle(UpdateApartment request, CancellationToken cancellationToken)
        {
            var currentApartment = _apartmentRepository.GetApartmentWithResidents(request.Id);

            if (currentApartment == null)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Apartamento não existe", NotificationType.Error));

                return Task.FromResult(false);
            }

            currentApartment.UpdateApartment(User.GetUserId(), request.Number, request.Block, request.Roof, request.CondominiumId);
                       
            if (!IsAValidApartment(currentApartment))
                return Task.FromResult(false);

            _apartmentRepository.Update(currentApartment);

            if (Commit())
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Operação realizada com sucesso", NotificationType.Info));

                return Task.FromResult(true);
            }

            Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Erro ao realizar a operação", NotificationType.Error));

            return Task.FromResult(false);
        }

        public Task<bool> Handle(DeleteApartment request, CancellationToken cancellationToken)
        {
            var currentApartment = _apartmentRepository.FindById(request.Id);

            if (currentApartment == null)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Apartamento não existe", NotificationType.Error));

                return Task.FromResult(false);
            }

            var hasResidents = _apartmentRepository.HasResidents(currentApartment.Id);

            if (hasResidents)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Apartamento com moradores não pode ser eliminado", NotificationType.Error));

                return Task.FromResult(false);
            }

            currentApartment.Delete(User.GetUserId());
            _apartmentRepository.Update(currentApartment);

            if (Commit())
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Operação realizada com sucesso", NotificationType.Info));

                return Task.FromResult(true);
            }

            Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Erro ao realizar a operação", NotificationType.Error));

            return Task.FromResult(false);
        }

        public Task<bool> Handle(RegisterResident request, CancellationToken cancellationToken)
        {
            var apartment = _apartmentRepository.GetApartmentWithResidents(request.ApartmentId);

            if (apartment == null)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Apartamento não existe", NotificationType.Error));

                return Task.FromResult(false);
            }

            var found = apartment.Residents.Any(r => r.Name == request.Name || r.Document.Number == request.DocumentNumber);

            if (found)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Resident), "Morador já existe neste apartamento", NotificationType.Error));

                return Task.FromResult(false);
            }

            var contact = new ContactInfo(request.PhoneNumber, request.Email);
            var document = new DocumentInfo(request.DocumentNumber);

            var newResident = Resident.ResidentFactory.NewResident(User.GetUserId(), request.Name, request.Birthday, contact, document, apartment.Id);

            if (!IsAValidResident(newResident))
                return Task.FromResult(false);

            apartment.AddResident(newResident);

            if (!IsAValidApartment(apartment))
                return Task.FromResult(false);

            _apartmentRepository.Update(apartment);

            if (Commit())
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Resident), "Operação realizada com sucesso", NotificationType.Info));

                return Task.FromResult(true);
            }

            Mediator.RaiseNotification(new DomainNotification(nameof(Resident), "Erro ao realizar a operação", NotificationType.Error));

            return Task.FromResult(false);
        }

        public Task<bool> Handle(UpdateResident request, CancellationToken cancellationToken)
        {
            var apartment = _apartmentRepository.GetApartmentWithResidents(request.ApartmentId);

            if (apartment == null)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Apartamento não existe", NotificationType.Error));

                return Task.FromResult(false);
            }

            var currentResident = apartment.Residents.FirstOrDefault(r => r.Id == request.Id);

            if (currentResident == null)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Resident), "Morador não existe neste apartamento", NotificationType.Error));

                return Task.FromResult(false);
            }

            var contact = new ContactInfo(request.PhoneNumber, request.Email);
            var document = new DocumentInfo(request.DocumentNumber);

            currentResident.UpdateResident(User.GetUserId(), request.Name, request.Birthday, contact, document, apartment.Id);

            if (!IsAValidResident(currentResident))
                return Task.FromResult(false);
            
            if (!IsAValidApartment(apartment))
                return Task.FromResult(false);

            _apartmentRepository.Update(apartment);

            if (Commit())
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Resident), "Operação realizada com sucesso", NotificationType.Info));

                return Task.FromResult(true);
            }

            Mediator.RaiseNotification(new DomainNotification(nameof(Resident), "Erro ao realizar a operação", NotificationType.Error));

            return Task.FromResult(false);
        }

        public Task<bool> Handle(DeleteResident request, CancellationToken cancellationToken)
        {
            var apartment = _apartmentRepository.GetApartmentWithResidents(request.ApartmentId);

            if (apartment == null)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Apartment), "Apartamento não existe", NotificationType.Error));

                return Task.FromResult(false);
            }

            var currentResident = apartment.Residents.FirstOrDefault(r => r.Id == request.Id && r.Status == Status.Active);

            if (currentResident == null)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Resident), "Morador não existe neste apartamento", NotificationType.Error));

                return Task.FromResult(false);
            }

            if (apartment.Residents.Where(r => r.Status == Status.Active).Count() == 1)
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Resident), "O apartamento não pode ficar vazio", NotificationType.Error));

                return Task.FromResult(false);
            }

            currentResident.Delete(User.GetUserId());
            _apartmentRepository.RemoveResident(currentResident);

            if (Commit())
            {
                Mediator.RaiseNotification(new DomainNotification(nameof(Resident), "Operação realizada com sucesso", NotificationType.Info));

                return Task.FromResult(true);
            }

            Mediator.RaiseNotification(new DomainNotification(nameof(Resident), "Erro ao realizar a operação", NotificationType.Error));

            return Task.FromResult(false);
        }

        private bool IsAValidApartment(IValidation apartment)
        {
            if (apartment.IsValid())
                return true;

            if(apartment.ValidationResult.Errors.Count() > 0)
                Mediator.RaiseNotification(new DomainNotification(apartment.ValidationResult.Errors[0].PropertyName, apartment.ValidationResult.Errors[0].ErrorMessage, NotificationType.Error));
            return false;
        }

        private bool IsAValidResident(IValidation resident)
        {
            if (resident.IsValid())
                return true;

            Mediator.RaiseNotification(new DomainNotification(resident.ValidationResult.Errors[0].PropertyName, resident.ValidationResult.Errors[0].ErrorMessage, NotificationType.Error));
            return false;
        }
    }
}
