using InstantBites.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Domain.Entites
{
    public class RestaurantLocation : BaseEntity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Id { get; set; }             
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
