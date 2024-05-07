using InstantBites.Domain.Entites.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Domain.Entites
{    
    public class Meal : BaseEntity
    {
        public string Name { get; set; }
        public double Calories { get; set; }
        public string? SavedUrl { get; set; }
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }        
        public double Weight { get; set; }
        public string MealCategoryId { get; set; }       
        public string Id { get; set; }
        public virtual ICollection<OrderMeals> Orders { get; set; }
        public virtual MealCategory MealCategory { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
