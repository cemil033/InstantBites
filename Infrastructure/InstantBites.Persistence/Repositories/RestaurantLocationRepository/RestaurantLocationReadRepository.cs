using InstantBites.Application.Repositories.RestaurantLocationRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using InstantBites.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.RestaurantLocationRepository
{
    public class RestaurantLocationReadRepository:ReadRepository<RestaurantLocation>, IRestaurantLocationReadRepository
    {
        public RestaurantLocationReadRepository(AppDBContext context) : base(context)
        {
        }
        
    }
}
