using InstantBites.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Domain.Entites
{
    public class OrderMeals:BaseEntity
    {
        public string OrderId { get; set; }
        public string MealId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Meal Meal { get; set; }
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
