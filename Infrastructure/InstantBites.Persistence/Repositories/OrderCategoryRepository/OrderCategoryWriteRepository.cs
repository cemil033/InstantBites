using InstantBites.Application.Repositories.OrderCategoryRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.OrderCategoryRepository
{
    public class OrderCategoryWriteRepository : WriteRepository<OrderCategory>, IOrderCategoryWriteRepository
    {
        public OrderCategoryWriteRepository(AppDBContext context) : base(context)
        {
        }
    }
}
