using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Restaurants.GetRestaurant
{
    public class GetRestaurantQueryResponse
    {
        public bool Success { get; set; }   
        public Restaurant Restaurant { get; set; }
    }
}
