using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Orders.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = _unitOfWork.R_Order.GetAll(tracking: false).ToList();
                return new() { Orders = orders };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
