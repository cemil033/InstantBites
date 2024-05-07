using InstantBites.Application.Repositories.ClientRepository;
using InstantBites.Application.Repositories.ClientLocationRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using InstantBites.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.ClientLocationRepository
{
    public class ClientLocationWriteRepository : WriteRepository<ClientLocation>, IClientLocationWriteRepository
    {
        public ClientLocationWriteRepository(AppDBContext context) : base(context)
        {
        }
    }
}
