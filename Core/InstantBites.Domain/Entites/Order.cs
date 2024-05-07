using InstantBites.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Domain.Entites
{
    public class Order:BaseEntity
    {    
        public string Name { get; set; }

        public double Price { get; set; }
        public double TotalWeight { get; set; }
        public double TotalCalories { get; set; }
        public string CategoryId { get; set; }
        public string RestaurantId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<OrderMeals> OrderMeals { get; set; }
        public virtual ICollection<ClientOrders> ClientOrders { get; set; }
        public virtual OrderCategory Category { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public string? SavedUrl { get; set; }
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

    }
}
