using InstantBites.Application.Repositories.OrderMealsRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.OrderMealsRepository
{
    public class OrderMealsReadRepository : ReadRepository<OrderMeals>, IOrderMealsReadRepository
    {
        public OrderMealsReadRepository(AppDBContext context) : base(context)
        {
        }
       
    }
}
