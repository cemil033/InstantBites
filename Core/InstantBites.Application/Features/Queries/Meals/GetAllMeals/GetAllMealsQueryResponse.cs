﻿using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Meals.GetAllMeals
{
    public class GetAllMealsQueryResponse
    {       
        public List<Meal> Meals { get; set; }
    }
}
