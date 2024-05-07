using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Clients.ClearClientNotifications
{
    public class ClearClientNotificationsCommandRequest : IRequest<ClearClientNotificationsCommandResponse>
    {
    }
}
