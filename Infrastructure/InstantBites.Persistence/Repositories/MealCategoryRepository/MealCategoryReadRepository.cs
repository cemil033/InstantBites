using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.MealCategoryRepository
{
    public class MealCategoryReadRepository : ReadRepository<MealCategory>, IMealCategoryReadRepository
    {
        public MealCategoryReadRepository(AppDBContext context) : base(context)
        {
        }
       
    }
}
