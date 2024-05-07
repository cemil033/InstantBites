using InstantBites.Application.Repositories.OrderCategoryRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.OrderCategoryRepository
{
    public class OrderCategoryReadRepository : ReadRepository<OrderCategory>, IOrderCategoryReadRepository
    {
        public OrderCategoryReadRepository(AppDBContext context) : base(context)
        {
        }
       
    }
}
