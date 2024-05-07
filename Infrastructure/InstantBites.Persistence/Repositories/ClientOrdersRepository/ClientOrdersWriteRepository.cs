using InstantBites.Application.Repositories.ClientOrdersRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.ClientOrdersRepository
{
    public class ClientOrdersWriteRepository : WriteRepository<ClientOrders>, IClientOrdersWriteRepository
    {
        public ClientOrdersWriteRepository(AppDBContext context) : base(context)
        {
        }
    }
}
