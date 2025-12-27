using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Plaground.Infrastructure.Persistence;
using Plaground.Infrastructure.Persistence.Repositories;
using Playground.Application.Contracts.Persistence;
using Playground.Domain.Entities;

namespace Plaground.Infrastructure;
public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public void AddInfrastructure(string connectionString)
        {
            services.AddPersistence(connectionString);
        }

        private void AddPersistence(string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddRepositories();
        }
        private void AddRepositories()
        {
            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<CodeBar>, Repository<CodeBar>>();
        }
    }
}