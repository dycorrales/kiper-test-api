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
using Kiper.Condominio.CrossCutting.Identity.Repositories;

namespace Kiper.Condominio.WebApi.Controllers
{
    [Route("users")]
    public class UserController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IUser _user;
        private readonly IMediatorBus _mediator;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, IMediatorBus mediator, INotificationHandler<DomainNotification> notifications, ILoggerFactory loggerFactory, IUser user) : base(mediator, notifications, loggerFactory, user)
        {
            _user = user;
            _mediator = mediator;
            _userRepository = userRepository;
            _logger = loggerFactory.CreateLogger("UserController");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [AuthorizeRoles(Roles.ADMIN)]
        [SwaggerOperation(Summary = "User By Email", Tags = new[] { "Users" })]
        public IActionResult GetUser()
        {
            try
            {
                var user = _userRepository.GetUser(_user.GetUserId()).Result;

                var userViewModel = new UserViewModel()
                {
                    Email = user.Email,
                    IsAdmin = _user.IsAdminUser,
                    Name = user.UserName
                };

                return IsAValidOperation()
                    ? RequestResponse(HttpStatusCode.OK, result: userViewModel)
                    : RequestResponse(HttpStatusCode.NotFound, "kipercondominio/api/v1/users", isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao procurar o usuário");
            }
        }
    }
}
