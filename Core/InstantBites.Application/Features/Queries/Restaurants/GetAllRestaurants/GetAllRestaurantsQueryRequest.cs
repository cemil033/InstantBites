﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Restaurants.GetAllRestaurants
{
    public class GetAllRestaurantsQueryRequest:IRequest<GetAllRestaurantsQueryResponse>
    {
    }
}
