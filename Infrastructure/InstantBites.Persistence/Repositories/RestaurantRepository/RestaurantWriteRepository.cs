using InstantBites.Application.Repositories.RestaurantRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.RestaurantRepository
{
    public class RestaurantWriteRepository : WriteRepository<Restaurant>, IRestaurantWriteRepository
    {
        public RestaurantWriteRepository(AppDBContext context) : base(context)
        {
        }
    }
}
