using FluxSYS_backend.Application.AppServices;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Repositories;
using FluxSYS_backend.Infrastructure.Repositories;

namespace FluxSYS_backend.Infraestructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar Repositorios
            services.AddScoped<IErrorLog, ErrorLogRepository>();
            services.AddScoped<ICompanies, CompaniesRepository>();
            services.AddScoped<IDepartments, DepartmentsRepository>();
            services.AddScoped<IClasificationMovements, ClasificationMovementsRepository>();
            services.AddScoped<ICategoriesProducts, CategoriesProductsRepository>();

            // Registrar Servicios
            services.AddScoped<ErrorLogService>();
            services.AddScoped<CompaniesService>();
            services.AddScoped<DepartmentsService>();
            services.AddScoped<ClasificationMovementsService>();
            services.AddScoped<CategoriesProductsService>();
            

            return services;
        }
    }
}
