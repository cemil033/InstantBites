using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Clients.ConfirmEmailClient
{
    public class ConfirmEmailClientCommandRequest:IRequest<ConfirmEmailClientCommandResponse>
    {
        public string Email { get; set; }
    }
}
