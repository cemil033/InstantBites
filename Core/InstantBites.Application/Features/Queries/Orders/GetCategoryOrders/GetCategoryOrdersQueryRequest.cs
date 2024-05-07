using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Orders.GetCategoryOrders
{
    public class GetCategoryOrdersQueryRequest:IRequest<GetCategoryOrdersQueryResponse>
    {
        public string Id { get; set; }
    }
}
