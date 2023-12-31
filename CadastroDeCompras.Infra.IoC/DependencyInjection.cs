using CadastroDeCompras.Application.Mappings;
using CadastroDeCompras.Application.Services;
using CadastroDeCompras.Application.Services.Interface;
using CadastroDeCompras.Domain.Authentication;
using CadastroDeCompras.Domain.Repositories;
using CadastroDeCompras.Infra.Data.Authentication;
using CadastroDeCompras.Infra.Data.Context;
using CadastroDeCompras.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CadastroDeCompras.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructre(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));
            
            services.AddSingleton(d => configuration);
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseMySql(connectionString, serverVersion)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            });
            
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenGeneretor, TokenGenerator>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}