using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Orders.ClientGetOrder
{
    public class ClientGetOrderQueryResponse
    {
        public bool Success { get; set; }
        public Order Order { get; set; }
        public List<Meal> Meals { get; set; }
        public double Shipping { get; set; }
    }
}
