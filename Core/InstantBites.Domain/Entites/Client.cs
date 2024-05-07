using InstantBites.Domain.Entites.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Domain.Entites
{
    public class Client:IdentityUser,BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Balance { get; set; }
        public string? ClientLocationId { get; set; }
        public virtual ClientLocation? ClientLocation { get; set; }
        public bool Active { get; set; }
        public string? SavedUrl { get; set; }
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<ClientOrders> Orders { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public override string Id { get; set; }
    }
}
