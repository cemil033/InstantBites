using InstantBites.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Domain.Entites
{
    public class Restaurant:BaseEntity
    {
        public string Name { get; set; }
        public string? RestaurantLocationId { get; set; }
        public virtual RestaurantLocation? RestaurantLocation { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public string? SavedUrl { get; set; }
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }
        public string? Email { get; set; }
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
