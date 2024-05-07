using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Orders.StopSubscribeOrder
{
    public class StopSubscribeOrderCommandRequest:IRequest<StopSubscribeOrderCommandResponse>
    {
        public string OrderId { get; set; }
    }
}
