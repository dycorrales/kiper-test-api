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
    [Route("condominiums")]
    public class CondominiumController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IUser _user;
        private readonly IMediatorBus _mediator;

        public CondominiumController(IMediatorBus mediator, INotificationHandler<DomainNotification> notifications, ILoggerFactory loggerFactory, IUser user) : base(mediator, notifications, loggerFactory, user)
        {
            _user = user;
            _mediator = mediator;
            _logger = loggerFactory.CreateLogger("CondominiumController");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Condominiums", Tags = new[] { "Condominiums" })]
        public IActionResult GetCondominiums([FromServices]CondominiumDao condominiumDao, [FromQuery] string name)
        {
            try
            {
                var condominiums = condominiumDao.ListOfCondominiums(name);

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, result: condominiums)
                    : RequestResponse(HttpStatusCode.NotFound, "kipercondominio/api/v1/condominiums", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao listar os condomínios");
            }
        }

        [HttpGet]
        [Route("all")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "All Condominiums By Condominium Id", Tags = new[] { "Condominiums" })]
        public IActionResult GetAllCondominiums([FromServices]CondominiumDao condominiumDao)
        {
            try
            {
                var condominiums = condominiumDao.ListOfAllCondominiums();

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, result: condominiums)
                    : RequestResponse(HttpStatusCode.NotFound, "kipercondominio/api/v1/condominiums", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao listar os condomínios");
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Condominium By Id", Tags = new[] { "Condominiums" })]
        public IActionResult GetCondominiumById([FromServices]CondominiumDao condominiumDao, [FromRoute] Guid id)
        {
            try
            {
                var condominium = condominiumDao.GetCondominiumById(id);

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, result: condominium)
                    : RequestResponse(HttpStatusCode.NotFound, "kipercondominio/api/v1/condominiums", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao obter a conta");
            }
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Register Condominium", Tags = new[] { "Condominiums" })]
        public async Task<IActionResult> RegisterCondominium([FromBody] CondominiumViewModel condominium)
        {
            try
            {
                await _mediator.SendCommand(new RegisterCondominium()
                {
                    Name = condominium.Name,
                    AddressStreet = condominium.Address.Street,
                    AddressNumber = condominium.Address.Number,
                    AddressCity = condominium.Address.City,
                    AddressComplement = condominium.Address.Complement,
                    AddressNeighbourhood = condominium.Address.Neighbourhood,
                    AddressState = condominium.Address.State,
                    AddressZipCode = condominium.Address.ZipCode
                });

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.Created, "kipercondominio/api/v1/condominiums")
                    : RequestResponse(HttpStatusCode.BadRequest, "kipercondominio/api/v1/condominiums", isError: true);
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
        [SwaggerOperation(Summary = "Update Condominium", Tags = new[] { "Condominiums" })]
        public async Task<IActionResult> UpdateCondominium([FromRoute] Guid id, [FromBody] CondominiumViewModel condominium)
        {
            try
            {
                await _mediator.SendCommand(new UpdateCondominium()
                {
                    Id = id,
                    Name = condominium.Name,
                    AddressStreet = condominium.Address.Street,
                    AddressNumber = condominium.Address.Number,
                    AddressCity = condominium.Address.City,
                    AddressComplement = condominium.Address.Complement,
                    AddressNeighbourhood = condominium.Address.Neighbourhood,
                    AddressState = condominium.Address.State,
                    AddressZipCode = condominium.Address.ZipCode
                });

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, "kipercondominio/api/v1/condominiums")
                    : RequestResponse(HttpStatusCode.BadRequest, "kipercondominio/api/v1/condominiums", isError: true);
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
        [SwaggerOperation(Summary = "Delete Condominium", Tags = new[] { "Condominiums" })]
        public async Task<IActionResult> DeleteCondominium([FromRoute] Guid id)
        {
            try
            {
                await _mediator.SendCommand(new DeleteCondominium()
                {
                    Id = id
                });

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, "kipercondominio/api/v1/condominiums")
                    : RequestResponse(HttpStatusCode.BadRequest, "kipercondominio/api/v1/condominiums", isError: true);
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
