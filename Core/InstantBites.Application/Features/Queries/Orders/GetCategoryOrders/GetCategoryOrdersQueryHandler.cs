using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Orders.GetCategoryOrders
{
    public class GetCategoryOrdersQueryHandler : IRequestHandler<GetCategoryOrdersQueryRequest, GetCategoryOrdersQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }       

        public async Task<GetCategoryOrdersQueryResponse> Handle(GetCategoryOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = _unitOfWork.R_Order.GetWhere(e => e.CategoryId == request.Id).ToList();
                return new() { Success = true,Orders= orders};
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
