using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.MealCategoryRepository
{
    public class MealCategoryWriteRepository : WriteRepository<MealCategory>, IMealCategoryWriteRepository
    {
        public MealCategoryWriteRepository(AppDBContext context) : base(context)
        {
        }
    }
}
