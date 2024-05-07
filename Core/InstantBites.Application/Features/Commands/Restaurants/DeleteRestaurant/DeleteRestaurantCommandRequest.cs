using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Restaurants.DeleteRestaurant
{
    public class DeleteRestaurantCommandRequest:IRequest<DeleteRestaurantCommandResponse>
    {
        public string Id { get; set; }
    }
}
