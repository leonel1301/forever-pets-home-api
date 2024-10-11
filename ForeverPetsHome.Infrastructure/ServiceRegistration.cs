using ForeverPetsHome.Application.Command.AdoptionManagement;
using ForeverPetsHome.Application.Command.UserManagement;
using ForeverPetsHome.Application.Interfaces;
using ForeverPetsHome.Application.Security;
using ForeverPetsHome.Infrastructure.Database;
using ForeverPetsHome.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ForeverPetsHome.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IAdoptionRepository, AdoptionRepository>();
            services.AddScoped<UserManagement>();
            services.AddScoped<PetsManagement>();
            services.AddScoped<AdoptionManagement>();
            #endregion
        }
    }
}