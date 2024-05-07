using InstantBites.Application.Repositories.RestaurantRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Restaurants.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQueryRequest, GetAllRestaurantsQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRestaurantsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<GetAllRestaurantsQueryResponse> Handle(GetAllRestaurantsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var restaurants = _unitOfWork.R_Restaurant.GetAll(false).ToList();
                var restloc = _unitOfWork.R_RestaurantLocation.GetAll(false).ToList();
                foreach (var restaurant in restaurants)
                {
                    restaurant.RestaurantLocation = restloc.FirstOrDefault(e => e.Id == restaurant.RestaurantLocationId);
                }

                return new() { Restaurants = restaurants };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
