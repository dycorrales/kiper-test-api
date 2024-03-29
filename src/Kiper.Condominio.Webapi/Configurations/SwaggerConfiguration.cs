﻿using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
namespace Kiper.Condominio.WebApi.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Kiper Condominios",
                    Description = "API Kiper Condominios"
                });

                s.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

            services.ConfigureSwaggerGen(opt =>
            {
                opt.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
        }
    }
}
