using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.OrderCategories.DeleteOrderCategory
{
    public class DeleteOrderCategoryCommandRequest:IRequest<DeleteOrderCategoryCommandResponse>
    {
        public string Id { get; set; }
    }
}
