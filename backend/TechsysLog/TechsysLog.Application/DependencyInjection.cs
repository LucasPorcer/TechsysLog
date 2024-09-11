using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TechsysLog.Application.Generators.Jwt;
using TechsysLog.Application.WebServices.ViaCep;
using TechsysLog.Application.WebServices.ViaCep.Interfaces;
using TechsysLog.Common.Middleware;
using TechsysLog.Domain.Interfaces.Jwt;

namespace TechsysLog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, ServiceLifetime lifeTime = ServiceLifetime.Scoped)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

          
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }

        public static IServiceCollection AddWebServices(this IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Scoped)
        {
            services.AddTransient<IViaCepService, ViaCepService>();

            return services;
        }
    }
}
