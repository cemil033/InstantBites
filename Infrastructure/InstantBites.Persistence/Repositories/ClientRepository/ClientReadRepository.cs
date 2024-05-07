using InstantBites.Application.Repositories.ClientRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.ClientRepository
{
    public class ClientReadRepository : ReadRepository<Client>, IClientReadRepository
    {
        public ClientReadRepository(AppDBContext context) : base(context)
        {
        }
       
    }
}
