using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Core.Notifications;
using Kiper.Condominio.CrossCutting.Identity.Models;
using Kiper.Condominio.CrossCutting.Identity.Repositories;
using Kiper.Condominio.Data.Query.Daos;
using Kiper.Condominio.Domain.Bus;
using Kiper.Condominio.Domain.Commands;
using Kiper.Condominio.Domain.Handlres;
using Kiper.Condominio.Domain.Interfaces;
using Kiper.Condominio.Infra.Data.Repositories;
using Kiper.Condominio.Infra.Data.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Kiper.Condominio.Infra.CrossCutting.IoC
{
    public class DependencyInjectionResolver
    {
        public static void RegisterServices(IServiceCollection services)
        { 
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IMediatorBus, MediatorBus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRequestHandler<RegisterCondominium, bool>, CondominiumHandler>();
            services.AddScoped<IRequestHandler<UpdateCondominium, bool>, CondominiumHandler>();
            services.AddScoped<IRequestHandler<DeleteCondominium, bool>, CondominiumHandler>();
            services.AddScoped<IRequestHandler<RegisterApartment, bool>, ApartmentHandler>();
            services.AddScoped<IRequestHandler<UpdateApartment, bool>, ApartmentHandler>();
            services.AddScoped<IRequestHandler<DeleteApartment, bool>, ApartmentHandler>();
            services.AddScoped<IRequestHandler<RegisterResident, bool>, ApartmentHandler>();
            services.AddScoped<IRequestHandler<UpdateResident, bool>, ApartmentHandler>();
            services.AddScoped<IRequestHandler<DeleteResident, bool>, ApartmentHandler>();

            services.AddScoped<ICondominiumRepository, CondominiumRepository>();
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient<CondominiumDao>();
            services.AddTransient<ApartmentDao>();
            services.AddTransient<DashboardDao>();
            services.AddTransient<ResidentDao>();

            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
