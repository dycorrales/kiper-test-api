using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using Kiper.Condominio.WebApi.Helpers;
using System.Threading.Tasks;
using Kiper.Condominio.Domain.Interfaces;
using Kiper.Condominio.Core.Interfaces;
using MediatR;
using Kiper.Condominio.Core.Notifications;
using Kiper.Condominio.CrossCutting.Identity.Models;
using Kiper.Condominio.Data.Query.Daos;
using Kiper.Condominio.WebApi.ViewModels;
using Kiper.Condominio.Domain.Commands;

namespace Kiper.Condominio.WebApi.Controllers
{
    [Route("{condominiumId:Guid}/apartments")]
    public class ApartmentController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IUser _user;
        private readonly IMediatorBus _mediator;

        public ApartmentController(IMediatorBus mediator, INotificationHandler<DomainNotification> notifications, ILoggerFactory loggerFactory, IUser user) : base(mediator, notifications, loggerFactory, user)
        {
            _user = user;
            _mediator = mediator;
            _logger = loggerFactory.CreateLogger("ApartmentController");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Apartments By Condominium Id", Tags = new[] { "Apartments" })]
        public IActionResult GetApartmentsByCondominiumId([FromServices]ApartmentDao apartmentDao, [FromRoute] Guid condominiumId)
        {
            try
            {
                var apartments = apartmentDao.ListOfApartmentsByCondominiumId(condominiumId);

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, result: apartments)
                    : RequestResponse(HttpStatusCode.NotFound, "kipercondominio/api/v1/apartments", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao listar os apartamentos");
            }
        }

        [HttpGet]
        [Route("all")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "All Apartments By Condominium Id", Tags = new[] { "Apartments" })]
        public IActionResult GetAllApartmentsByCondominiumId([FromServices]ApartmentDao apartmentDao, [FromRoute] Guid condominiumId)
        {
            try
            {
                var apartments = apartmentDao.ListOfAllApartmentsByCondominiumId(condominiumId);

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, result: apartments)
                    : RequestResponse(HttpStatusCode.NotFound, "kipercondominio/api/v1/apartments", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao listar os apartamentos");
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Apartment By Id", Tags = new[] { "Apartments" })]
        public IActionResult GetApartmentById([FromServices]ApartmentDao apartmentDao, [FromRoute] Guid id)
        {
            try
            {
                var apartment = apartmentDao.GetApartmentById(id);

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, result: apartment)
                    : RequestResponse(HttpStatusCode.NotFound, "kipercondominio/api/v1/apartments", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao obter o apartamento");
            }
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Register Apartment", Tags = new[] { "Apartments" })]
        public async Task<IActionResult> RegisterApartment([FromRoute] Guid condominiumId, [FromBody] RegisterApartmentViewModel apartment)
        {
            try
            {
                await _mediator.SendCommand(new RegisterApartment()
                {
                    Block = apartment.Block,
                    Number = apartment.Number,
                    Roof = apartment.Roof,
                    CondominiumId = condominiumId,
                    Residents = apartment.Residents.Select(r => new ResidentCommand()
                    {
                        Name = r.Name,
                        Birthday = r.Birthday,
                        DocumentNumber = r.DocumentNumber,
                        Email = r.Email,
                        PhoneNumber = r.PhoneNumber
                    })
                });

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.Created, "kipercondominio/api/v1/apartments")
                    : RequestResponse(HttpStatusCode.BadRequest, "kipercondominio/api/v1/apartments", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.InternalServerError, isError: true, result: "Ocorreu um erro ao realizar a operação");
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Update Apartment", Tags = new[] { "Apartments" })]
        public async Task<IActionResult> UpdateApartment([FromRoute] Guid condominiumId, [FromRoute] Guid id, [FromBody] ApartmentViewModel apartment)
        {
            try
            {
                await _mediator.SendCommand(new UpdateApartment()
                {
                    Id = id,
                    Block = apartment.Block,
                    Number = apartment.Number,
                    Roof = apartment.Roof,
                    CondominiumId = condominiumId
                });

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, "kipercondominio/api/v1/apartments")
                    : RequestResponse(HttpStatusCode.BadRequest, "kipercondominio/api/v1/apartments", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.InternalServerError, isError: true, result: "Ocorreu um erro ao realizar a operação");
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Delete Apartment", Tags = new[] { "Apartments" })]
        public async Task<IActionResult> DeleteApartment([FromRoute] Guid id)
        {
            try
            {
                await _mediator.SendCommand(new DeleteApartment()
                {
                    Id = id
                });

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, "kipercondominio/api/v1/apartments")
                    : RequestResponse(HttpStatusCode.BadRequest, "kipercondominio/api/v1/apartments", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.InternalServerError, isError: true, result: "Ocorreu um erro ao realizar a operação");
            }
        }

        [HttpGet]
        [Route("{id:Guid}/residents")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Resident By Apartment Id", Tags = new[] { "Apartments / Residents" })]
        public IActionResult GetResidentsByApartmentId([FromServices]ResidentDao residentDao, [FromRoute] Guid id)
        {
            try
            {
                var residents = residentDao.ListOfResidentsByApartmentId(id);

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, result: residents)
                    : RequestResponse(HttpStatusCode.NotFound, "kipercondominio/api/v1/apartments", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao obter os moradores");
            }
        }

        [HttpPost]
        [Route("{apartmentId:Guid}/residents")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Register Resident", Tags = new[] { "Apartments / Residents" })]
        public async Task<IActionResult> RegisterResident([FromRoute] Guid apartmentId, [FromBody] ResidentViewModel resident)
        {
            try
            {
                await _mediator.SendCommand(new RegisterResident()
                {
                    Name = resident.Name,
                    Email = resident.Email,
                    Birthday = resident.Birthday,
                    DocumentNumber = resident.DocumentNumber,
                    PhoneNumber = resident.PhoneNumber,
                    ApartmentId = apartmentId
                });

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.Created, "kipercondominio/api/v1/apartments")
                    : RequestResponse(HttpStatusCode.BadRequest, "kipercondominio/api/v1/apartments", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.InternalServerError, isError: true, result: "Ocorreu um erro ao realizar a operação");
            }
        }

        [HttpPut]
        [Route("{apartmentId:Guid}/residents/{id:Guid}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Update Resident", Tags = new[] { "Apartments / Residents" })]
        public async Task<IActionResult> UpdateResident([FromRoute] Guid apartmentId, [FromRoute] Guid id, [FromBody] ResidentViewModel resident)
        {
            try
            {
                await _mediator.SendCommand(new UpdateResident()
                {
                    Id = id,
                    Name = resident.Name,
                    Birthday = resident.Birthday,
                    DocumentNumber = resident.DocumentNumber,
                    Email = resident.Email,
                    PhoneNumber = resident.PhoneNumber,
                    ApartmentId = apartmentId
                });

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, "kipercondominio/api/v1/apartments")
                    : RequestResponse(HttpStatusCode.BadRequest, "kipercondominio/api/v1/apartments", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.InternalServerError, isError: true, result: "Ocorreu um erro ao realizar a operação");
            }
        }

        [HttpDelete]
        [Route("{apartmentId:Guid}/residents/{id:Guid}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Delete Resident", Tags = new[] { "Apartments / Residents" })]
        public async Task<IActionResult> DeleteResident([FromRoute] Guid apartmentId, [FromRoute] Guid id)
        {
            try
            {
                await _mediator.SendCommand(new DeleteResident()
                {
                    Id = id,
                    ApartmentId = apartmentId
                });

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, "kipercondominio/api/v1/apartments")
                    : RequestResponse(HttpStatusCode.BadRequest, "kipercondominio/api/v1/apartments", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.InternalServerError, isError: true, result: "Ocorreu um erro ao realizar a operação");
            }
        }
    }
}
