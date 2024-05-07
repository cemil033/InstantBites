using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Orders.ClientGetOrder
{
    public class ClientGetOrderQueryRequest:IRequest<ClientGetOrderQueryResponse>
    {
        public string Id { get; set; }
    }
}
