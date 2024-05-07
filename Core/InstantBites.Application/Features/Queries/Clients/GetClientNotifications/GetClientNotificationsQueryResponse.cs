using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Clients.GetClientNotifications
{
    public class GetClientNotificationsQueryResponse
    {
        public List<Notification> Notifications { get; set; }
    }
}
