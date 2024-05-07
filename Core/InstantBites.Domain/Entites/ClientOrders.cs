using InstantBites.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Domain.Entites
{
    
    public class ClientOrders:BaseEntity
    {
        public virtual Order Order { get; set; }
        public virtual Client Client { get; set; }
        public string ClientId { get; set; }
        public string OrderId { get; set; }
        public DateTime LastSubscribedTime { get; set; }
        public double Shipping { get; set; }
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
