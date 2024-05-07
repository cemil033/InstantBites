using InstantBites.Application.Repositories.ClientLocationRepository;
using InstantBites.Application.Repositories.ClientOrdersRepository;
using InstantBites.Application.Repositories.ClientRepository;
using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.Repositories.MealRepository;
using InstantBites.Application.Repositories.NotificationRepository;
using InstantBites.Application.Repositories.OrderCategoryRepository;
using InstantBites.Application.Repositories.OrderMealsRepository;
using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Application.Repositories.RestaurantLocationRepository;
using InstantBites.Application.Repositories.RestaurantRepository;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using InstantBites.Persistence.Repositories.ClientLocationRepository;
using InstantBites.Persistence.Repositories.ClientOrdersRepository;
using InstantBites.Persistence.Repositories.ClientRepository;
using InstantBites.Persistence.Repositories.MealCategoryRepository;
using InstantBites.Persistence.Repositories.MealRepository;
using InstantBites.Persistence.Repositories.NotificationRepository;
using InstantBites.Persistence.Repositories.OrderCategoryRepository;
using InstantBites.Persistence.Repositories.OrderMealsRepository;
using InstantBites.Persistence.Repositories.OrderRepository;
using InstantBites.Persistence.Repositories.RestaurantLocationRepository;
using InstantBites.Persistence.Repositories.RestaurantRepository;
using InstantBites.Persistence.Seed;
using InstantBites.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services)
        {
            services.AddDbContext<AppDBContext>(options => { options.UseSqlServer(Configuration.ConnectionString);options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); });

            services.AddIdentity<Client, IdentityRole>()
                    .AddEntityFrameworkStores<AppDBContext>()                    
                    .AddDefaultTokenProviders();
           
            services.AddScoped<IdentityDataSeeder>();
            services.AddHostedService<SetupIdentityDataSeeder>();

            services.AddScoped<IClientReadRepository, ClientReadRepository>();
            services.AddScoped<IClientWriteRepository, ClientWriteRepository>();

            services.AddScoped<IClientLocationReadRepository, ClientLocationReadRepository>();
            services.AddScoped<IClientLocationWriteRepository, ClientLocationWriteRepository>();

            services.AddScoped<IClientOrdersReadRepository, ClientOrdersReadRepository>();
            services.AddScoped<IClientOrdersWriteRepository, ClientOrdersWriteRepository>();

            services.AddScoped<IMealReadRepository, MealReadRepository>();
            services.AddScoped<IMealWriteRepository, MealWriteRepository>();

            services.AddScoped<IMealCategoryReadRepository, MealCategoryReadRepository>();
            services.AddScoped<IMealCategoryWriteRepository, MealCategoryWriteRepository>();

            services.AddScoped<INotificationReadRepository, NotificationReadRepository>();
            services.AddScoped<INotificationWriteRepository, NotificationWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IOrderMealsReadRepository, OrderMealsReadRepository>();
            services.AddScoped<IOrderMealsWriteRepository, OrderMealsWriteRepository>();

            services.AddScoped<IOrderCategoryReadRepository, OrderCategoryReadRepository>();
            services.AddScoped<IOrderCategoryWriteRepository, OrderCategoryWriteRepository>();

            services.AddScoped<IRestaurantReadRepository, RestaurantReadRepository>();
            services.AddScoped<IRestaurantWriteRepository, RestaurantWriteRepository>();
            services.AddScoped<IRestaurantLocationReadRepository, RestaurantLocationReadRepository>();
            services.AddScoped<IRestaurantLocationWriteRepository, RestaurantLocationWriteRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
