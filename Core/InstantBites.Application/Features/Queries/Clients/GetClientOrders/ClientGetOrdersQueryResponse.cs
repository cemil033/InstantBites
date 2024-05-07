using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Clients.GetClientOrders
{
    public class ClientGetOrdersQueryResponse
    {
        public bool Success { get; set; } 
        public List<ClientOrders> Orders { get; set; }
    }
}
