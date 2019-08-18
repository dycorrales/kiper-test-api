using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Core.Notifications;
using Kiper.Condominio.CrossCutting.Identity.Authorization;
using Kiper.Condominio.CrossCutting.Identity.Models;
using Kiper.Condominio.WebApi.Controllers;
using Kiper.Condominio.WebApi.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kiper.Condominio.Webapi.Controllers
{
    public class SecurityController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly TokenDescriptor _tokenDescriptor;

        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public SecurityController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TokenDescriptor tokenDescriptor, IUser user, IMediatorBus mediator, INotificationHandler<DomainNotification> notifications, ILoggerFactory loggerFactory, IUnitOfWork unitOfWork) : base(mediator, notifications, loggerFactory, user)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<SecurityController>();
            _tokenDescriptor = tokenDescriptor;
        }

        [HttpPost]
        [AllowAnonymous]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [Route("login")]
        [SwaggerOperation(Summary = "Login", Tags = new[] { "Security" })]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyInvalidModelError();
                    return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Os dados fornecidos são inválidos");
                }

                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                {
                    return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Os dados fornecidos são inválidos");
                }

                var userIdentity = _userManager.FindByEmailAsync(model.Email).Result;

                if (userIdentity != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(userIdentity, model.Password, false);
                    object accessToken = null;

                    if (result.Succeeded)
                    {
                        var info = JsonConvert.SerializeObject($"Usuário {userIdentity.Email} logado com sucesso!!");
                        _logger.LogInformation(info);

                        accessToken = await GenerateToken(userIdentity);

                        return RequestResponse(HttpStatusCode.OK, result: accessToken);
                    }
                }

                var message = $"Erro ao fazer login com usuário {model.Email}";

                var error = JsonConvert.SerializeObject($"Usuário {model.Email} não autorizado");
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.Unauthorized, isError: true);
            }
            catch (Exception ex)
            {
                var error = JsonConvert.SerializeObject(ex);
                _logger.LogError(error);

                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao fazer login");
            }
        }

        private async Task<object> GenerateToken(ApplicationUser userIdentity)
        {
            var userClaims = await _userManager.GetClaimsAsync(userIdentity);

            var roles = await _userManager.GetRolesAsync(userIdentity);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, userIdentity.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, userIdentity.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var role in roles)
                userClaims.Add(new Claim(ClaimTypes.Role, role));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var handler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentialsConfiguration();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenDescriptor.Issuer,
                Audience = _tokenDescriptor.Audience,
                SigningCredentials = signingConf.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
            });

            var encodedJwt = handler.WriteToken(securityToken);

            var response = new
            {
                access_token = encodedJwt,
                user_name = userIdentity.UserName
            };

            return response;
        }
    }
}