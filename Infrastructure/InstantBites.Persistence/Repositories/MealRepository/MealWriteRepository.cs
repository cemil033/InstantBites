using InstantBites.Application.Repositories.MealRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.MealRepository
{
    public class MealWriteRepository : WriteRepository<Meal>, IMealWriteRepository
    {
        public MealWriteRepository(AppDBContext context) : base(context)
        {
        }
    }
}
