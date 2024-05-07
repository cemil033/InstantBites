using InstantBites.Domain.Entites;
using InstantBites.Domain.Entites.Common;
using InstantBites.Persistence.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Context
{
    public class AppDBContext : IdentityDbContext<Client>
    {
        public AppDBContext(DbContextOptions options) : base(options){ }        

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientLocation> ClientLocations { get; set; }
        public DbSet<RestaurantLocation> RestaurantLocations { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<ClientOrders> ClientOrders { get; set; }
        public DbSet<MealCategory> MealCategories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<OrderCategory> OrderCategories { get; set; }
        public DbSet<OrderMeals>  OrderMeals { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedTime = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedTime = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return base.SaveChangesAsync(cancellationToken);

        }
        
    }
}


