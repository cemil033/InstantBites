using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Meals.DeleteMeal
{
    public class DeleteMealCommandRequest:IRequest<DeleteMealCommandResponse>
    {
        public string Id { get; set; }
    }
}
