using Kiper.Condominio.CrossCutting.Identity.Data;
using Kiper.Condominio.CrossCutting.Identity.Models;
using Kiper.Condominio.Domain.Interfaces;
using Kiper.Condominio.Infra.Data.Contexts;
using Kiper.Condominio.Infra.Data.Initializers;
using Kiper.Condominio.WebApi.Configurations;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;

namespace Kiper.Condominio.WebApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public static IConfiguration Config { get; set; }

        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Config = builder.Build();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcSecurity(Configuration);

            string kiperCondominioConnectionString = Configuration.GetConnectionString("kiperCondominioConnectionString");
            string kiperCondominioSecurityConnectionString = Configuration.GetConnectionString("kiperCondominioSecurityConnectionString");
            
            services.AddDbContext<SecurityContext>(options =>
                options.UseMySql(kiperCondominioSecurityConnectionString));

            services.AddDbContext<ApplicationContext>(options =>
                options.UseMySql(kiperCondominioConnectionString));

            services.AddOptions();
            services.AddResponseCaching();
            services.AddLogging();

            services.Configure<GzipCompressionProviderOptions>(
                opt => opt.Level = CompressionLevel.Optimal
                );

            services.AddResponseCompression(opt =>
            {
                opt.Providers.Add<GzipCompressionProvider>();
                opt.EnableForHttps = true;
            });

            services.AddMvc(opt =>
            {
                opt.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(opt => opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);

            services.AddCors();

            services.AddApiVersioning("kipercondominio/api/v{version}");

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
            });
            services.AddSwaggerConfig();

            services.AddMediatR(typeof(Startup));

            services.AddSingleton(provider => Configuration);
            services.AddDIConfiguration();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
            });

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IHttpContextAccessor accessor, SecurityContext identityContext, ApplicationContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            loggerFactory.AddLog4Net(Configuration.GetValue<string>("Log4NetConfigFile:Name"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            CultureInfo defaultCulture = new CultureInfo("es-UY");
            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };

            app.UseRequestLocalization(localizationOptions);

            app.UseCors(c =>
            {
                c.AllowAnyOrigin();
                c.AllowAnyHeader();
                c.AllowAnyMethod();
            });

            app.UseResponseCaching();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Kiper Condominio API v1.0");
                s.RoutePrefix = string.Empty;
            });

            new IdentityInitializer(userManager, identityContext, roleManager).Initialize();
            ApplicationUser adminUser = identityContext.Users.FirstOrDefaultAsync(u => u.Email == "condominio@kiper.com.br").Result;

            if (adminUser != null)
            {
                new DataBaseInitializer(context, new Guid(adminUser.Id)).Initialize();
            }
        }
    }
}
