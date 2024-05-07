using InstantBites.Application.Repositories.ClientRepository;
using InstantBites.Application.Repositories.RestaurantLocationRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using InstantBites.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.RestaurantLocationRepository
{
    public class RestaurantLocationWriteRepository : WriteRepository<RestaurantLocation>, IRestaurantLocationWriteRepository
    {
        public RestaurantLocationWriteRepository(AppDBContext context) : base(context)
        {
        }
    }
}
