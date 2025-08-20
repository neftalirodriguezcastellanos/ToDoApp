using ToDoList.Application.Data;
using ToDoList.Domain.Tasks;
using ToDoList.Domain.Primitives;
using ToDoList.Domain.Users;
using ToDoList.Infrastructure.Entities.Identity;
using ToDoList.Infrastructure.Persistence;
using ToDoList.Infrastructure.Persistence.Repositories;
using ToDoList.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SqlServer")
                            ?? throw new InvalidOperationException("Connection string 'SqlServer' not found.");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

            // DbContext como IApplicationDbContext
            services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddDataProtection();

            // ✅ Registrar UoW basado solo en EF
            services.AddScoped<IUnitOfWork>(sp =>
            {
                var context = sp.GetRequiredService<ApplicationDbContext>();
                var publisher = sp.GetRequiredService<IPublisher>();
                return new UnitOfWork(context, publisher);
            });

            // ✅ Repositorios
            services.AddScoped<ITaskRepository>(sp =>
            {
                var uow = sp.GetRequiredService<IUnitOfWork>();
                return uow.Tasks;
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}