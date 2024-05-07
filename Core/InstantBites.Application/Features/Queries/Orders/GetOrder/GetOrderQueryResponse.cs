using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Orders.GetOrder
{
    public class GetOrderQueryResponse
    {
        public bool Success { get; set; } 
        public Order Order { get; set; }
        public List<string> MealsId { get; set; }
    }
}
