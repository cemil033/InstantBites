using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.OrderCategories.GetOrderCategory
{
    public class GetOrderCategoryQueryRequest:IRequest<GetOrderCategoryQueryResponse>
    {
        public string Id { get; set; }
    }
}
