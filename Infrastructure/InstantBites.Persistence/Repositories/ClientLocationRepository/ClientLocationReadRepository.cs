using InstantBites.Application.Repositories.ClientLocationRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using InstantBites.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.ClientLocationRepository
{
    public class ClientLocationReadRepository:ReadRepository<ClientLocation>, IClientLocationReadRepository
    {
        public ClientLocationReadRepository(AppDBContext context) : base(context)
        {
        }
        
    }
}
