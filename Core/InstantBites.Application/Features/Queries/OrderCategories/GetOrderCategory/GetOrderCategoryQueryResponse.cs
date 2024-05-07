using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.OrderCategories.GetOrderCategory
{
    public class GetOrderCategoryQueryResponse
    {
        public bool Success { get; set; }
        public OrderCategory OrderCategory { get; set; }
    }
}
