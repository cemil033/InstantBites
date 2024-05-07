using InstantBites.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Domain.Entites
{
    public class OrderCategory:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
