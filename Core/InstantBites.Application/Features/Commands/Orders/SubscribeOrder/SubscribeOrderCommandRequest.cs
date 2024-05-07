using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Orders.SubscribeOrder
{
    public class SubscribeOrderCommandRequest : IRequest<SubscribeOrderCommandResponse>
    {
        public string Id { get; set; }
    }
}
