using InstantBites.Application.Repositories.MealRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.MealRepository
{
    public class MealReadRepository : ReadRepository<Meal>, IMealReadRepository
    {
        public MealReadRepository(AppDBContext context) : base(context)
        {
        }
        
    }
}
