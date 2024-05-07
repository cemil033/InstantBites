using InstantBites.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Domain.Entites
{
    public class Notification:BaseEntity
    {
        public string Message { get; set; }       
        public bool IsRead { get; set; }
        public virtual Client Client { get; set; }
        public string ClientId { get; set; }
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
