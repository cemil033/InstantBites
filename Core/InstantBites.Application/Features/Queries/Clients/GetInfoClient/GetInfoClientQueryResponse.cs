using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Clients.GetInfoClient
{
    public class GetInfoClientQueryResponse
    {
        public bool Success { get; set; } 
        public Client Client { get; set; }   
    }
}
