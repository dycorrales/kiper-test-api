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
    [Route("residents")]
    public class ResidentController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IUser _user;
        private readonly IMediatorBus _mediator;

        public ResidentController(IMediatorBus mediator, INotificationHandler<DomainNotification> notifications, ILoggerFactory loggerFactory, IUser user) : base(mediator, notifications, loggerFactory, user)
        {
            _user = user;
            _mediator = mediator;
            _logger = loggerFactory.CreateLogger("ResidentController");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Residents", Tags = new[] { "Residents" })]
        public IActionResult GetResidents([FromServices]ResidentDao residentDao, [FromQuery] string filter)
        {
            try
            {
                var residents = residentDao.ListOfResidents(filter);

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, result: residents)
                    : RequestResponse(HttpStatusCode.NotFound, "kipercondominio/api/v1/residents", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao listar os moradores");
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Resident By Id", Tags = new[] { "Residents" })]
        public IActionResult GetResidentById([FromServices]ResidentDao residentDao, [FromRoute] Guid id)
        {
            try
            {
                var residents = residentDao.GetResidentById(id);

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, result: residents)
                    : RequestResponse(HttpStatusCode.NotFound, "kipercondominio/api/v1/residents", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao obter o morador");
            }
        }
    }
}
