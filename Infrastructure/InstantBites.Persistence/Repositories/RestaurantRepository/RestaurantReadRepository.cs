using InstantBites.Application.Repositories.RestaurantRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.RestaurantRepository
{
    public class RestaurantReadRepository : ReadRepository<Restaurant>, IRestaurantReadRepository
    {
        public RestaurantReadRepository(AppDBContext context) : base(context)
        {
        }
       
    }
}
