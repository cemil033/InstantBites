using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.OrderCategories.UpdateOrderCategory
{
    public class UpdateOrderCategoryCommandRequest:IRequest<UpdateOrderCategoryCommandResponse>
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
