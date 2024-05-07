using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Meals.GetMeal
{
    public class GetMealQueryRequest:IRequest<GetMealQueryResponse>
    {
        public string Id { get; set; }
    }
}
