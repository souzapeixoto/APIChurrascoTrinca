
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
            services.AddScoped<IRepositoryConvidado, RepositoryConvidado>();
            services.AddScoped<IRepositoryOpcao, RepositoryOpcao>();
            services.AddScoped<IRepositoryAuth, RepositoryAuth>();
            services.AddScoped<IRepositoryUsuario, RepositoryUsuario>();
            services.AddScoped<IRepositoryChurrasco, RepositoryChurrasco>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            return services;
        }
    }
}
