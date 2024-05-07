using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.Repositories.OrderCategoryRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.OrderCategories.GetOrderCategory
{
    public class GetOrderCategoryQueryHandler : IRequestHandler<GetOrderCategoryQueryRequest, GetOrderCategoryQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<GetOrderCategoryQueryResponse> Handle(GetOrderCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.R_OrderCategory.Get(request.Id, false);
                if (category == null) return new() { Success = false };
                return new() { Success = true, OrderCategory = category };
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
