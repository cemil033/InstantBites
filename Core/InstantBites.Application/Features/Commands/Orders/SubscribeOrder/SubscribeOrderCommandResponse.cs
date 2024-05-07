using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Orders.SubscribeOrder
{
    public class SubscribeOrderCommandResponse
    {
        public bool Success { get; set; }
        public bool IsSubscribed { get; set; }
        public Restaurant Restaurant { get; set; }
        public Order Order { get; set; }
    }
}
