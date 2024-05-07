using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.Repositories.OrderCategoryRepository;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.OrderCategories.GetAllOrderCategories
{
    public class GetAllOrderCategoriesQueryHandler : IRequestHandler<GetAllOrderCategoriesQueryRequest, GetAllOrderCategoriesQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllOrderCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        

        public async Task<GetAllOrderCategoriesQueryResponse> Handle(GetAllOrderCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var categoires = _unitOfWork.R_OrderCategory.GetAll(tracking: false).ToList();
                return new()
                {
                    OrderCategories = categoires
                };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
