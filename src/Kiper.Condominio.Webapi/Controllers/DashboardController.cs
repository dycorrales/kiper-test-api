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
    [Route("dashboard")]
    public class DashboardController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IUser _user;
        private readonly IMediatorBus _mediator;

        public DashboardController(IMediatorBus mediator, INotificationHandler<DomainNotification> notifications, ILoggerFactory loggerFactory, IUser user) : base(mediator, notifications, loggerFactory, user)
        {
            _user = user;
            _mediator = mediator;
            _logger = loggerFactory.CreateLogger("DashboardController");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "Dashboard", Tags = new[] { "Dashboard" })]
        public IActionResult GetCondominiums([FromServices]DashboardDao dashboardDao)
        {
            try
            {
                var dashboard = dashboardDao.DashboardInfo();

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, result: dashboard)
                    : RequestResponse(HttpStatusCode.NotFound, "kipercondominio/api/v1/dashboard", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao carregar o dashboard");
            }
        }
    }
}
