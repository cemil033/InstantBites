using InstantBites.Application.Repositories.OrderMealsRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.OrderMealsRepository
{
    public class OrderMealsWriteRepository : WriteRepository<OrderMeals>, IOrderMealsWriteRepository
    {
        public OrderMealsWriteRepository(AppDBContext context) : base(context)
        {
        }
    }
}
