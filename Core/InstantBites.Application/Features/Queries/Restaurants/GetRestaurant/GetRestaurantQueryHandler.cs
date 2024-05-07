using InstantBites.Application.Repositories.RestaurantRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Restaurants.GetRestaurant
{
    public class GetRestaurantQueryHandler : IRequestHandler<GetRestaurantQueryRequest, GetRestaurantQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRestaurantQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetRestaurantQueryResponse> Handle(GetRestaurantQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var restaurant = await _unitOfWork.R_Restaurant.Get(request.Id, false);
                var location = await _unitOfWork.R_RestaurantLocation.Get(restaurant.RestaurantLocationId);
                restaurant.RestaurantLocation = location;
                if (restaurant == null) return new() { Success = false };
                return new() { Success = true, Restaurant = restaurant };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
