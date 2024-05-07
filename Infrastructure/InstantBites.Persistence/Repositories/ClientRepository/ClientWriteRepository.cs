using InstantBites.Application.Repositories;
using InstantBites.Application.Repositories.ClientRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.ClientRepository
{
    public class ClientWriteRepository : WriteRepository<Client>, IClientWriteRepository
    {
        public ClientWriteRepository(AppDBContext context) : base(context)
        {
        }
    }
}
