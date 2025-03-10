using FluxSYS_backend.Application.AppServices;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Repositories;

namespace FluxSYS_backend.Infraestructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar Repositorios
            services.AddScoped<IErrorLog, ErrorLogRepository>();
            services.AddScoped<ICompanies, CompaniesRepository>();

            // Registrar Servicios
            services.AddScoped<ErrorLogService>();
            services.AddScoped<CompaniesService>();

            return services;
        }
    }
}
