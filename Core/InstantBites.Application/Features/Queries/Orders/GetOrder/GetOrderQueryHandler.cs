using InstantBites.Application.Repositories.OrderMealsRepository;
using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Orders.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQueryRequest, GetOrderQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }        

        public async Task<GetOrderQueryResponse> Handle(GetOrderQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.R_Order.Get(request.Id, false);
                List<string> ids = new();
                foreach (var item in _unitOfWork.R_OrderMeals.GetAll())
                {
                    if (item.OrderId == request.Id)
                    {
                        ids.Add(item.MealId);
                    }
                }
                if (order == null) return new() { Success = false };
                return new() { Success = true, Order = order, MealsId = ids };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
