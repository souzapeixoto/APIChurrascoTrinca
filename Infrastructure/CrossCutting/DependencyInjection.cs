
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CrossCutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            //repositorios
            services.AddScoped<IRepositoryAuth, RepositoryAuth>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            return services;
        }
    }
}
