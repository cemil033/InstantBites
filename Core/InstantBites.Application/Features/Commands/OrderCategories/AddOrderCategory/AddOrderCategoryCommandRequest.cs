using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.OrderCategories.AddOrderCategory
{
    public class AddOrderCategoryCommandRequest:IRequest<AddOrderCategoryCommandResponse>
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
