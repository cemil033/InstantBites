﻿using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Restaurants.GetAllRestaurants
{
    public class GetAllRestaurantsQueryResponse
    {
        public List<Restaurant> Restaurants { get; set; }
        
    }
}
