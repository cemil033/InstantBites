using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Orders.GetRestaurantOrders
{
    public class GetRestaurantOrdersQueryHandler : IRequestHandler<GetRestaurantOrdersQueryRequest, GetRestaurantOrdersQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRestaurantOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }                

        public async Task<GetRestaurantOrdersQueryResponse> Handle(GetRestaurantOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = _unitOfWork.R_Order.GetWhere(e => e.RestaurantId == request.Id).ToList();
                return new() { Success = true, Orders = orders };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
