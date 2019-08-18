using System;
using Microsoft.Extensions.Logging;
using Kiper.Condominio.Core.Helpers;
using Kiper.Condominio.Core.Notifications;
using Kiper.Condominio.Infra.Data.Contexts;
using Kiper.Condominio.Core.Interfaces;

namespace Kiper.Condominio.Infra.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly IMediatorBus _mediatorHandler;
        private readonly ILogger _logger;

        public UnitOfWork(ApplicationContext context, IMediatorBus mediatorHandler, ILoggerFactory logger)
        {
            _context = context;
            _mediatorHandler = mediatorHandler;
            _logger = logger.CreateLogger<UnitOfWork>();
        }

        public Response Commit()
        {
            try
            {
                return _context.SaveChanges() > 0 ? Response.Ok : Response.Fail;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                _logger.LogError($"On saving in database. {DateTime.Today}");
                _mediatorHandler.RaiseNotification(new DomainNotification("Error", ex.Message, NotificationType.Error));

                return Response.Fail;
            }
        }
    }
}
