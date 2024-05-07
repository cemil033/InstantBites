using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.OrderRepository
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(AppDBContext context) : base(context)
        {
        }
    }
}
