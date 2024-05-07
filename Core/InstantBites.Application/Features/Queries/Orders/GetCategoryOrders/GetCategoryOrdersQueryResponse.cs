using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Orders.GetCategoryOrders
{
    public class GetCategoryOrdersQueryResponse
    {
        public bool Success { get; set; }
        public List<Order> Orders { get; set; }
    }
}
