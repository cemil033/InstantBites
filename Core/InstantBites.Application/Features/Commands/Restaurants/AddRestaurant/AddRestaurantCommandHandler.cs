using InstantBites.Application.Repositories.RestaurantLocationRepository;
using InstantBites.Application.Repositories.RestaurantRepository;
using InstantBites.Application.Services;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using InstantBites.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Restaurants.AddRestaurant
{
    public class AddRestaurantCommandHandler : IRequestHandler<AddRestaurantCommandRequest, AddRestaurantCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PhotoManager _photoManager;

        public AddRestaurantCommandHandler(IUnitOfWork unitOfWork, PhotoManager photoManager)
        {
            _unitOfWork = unitOfWork;
            _photoManager = photoManager;
        }
                
        public async Task<AddRestaurantCommandResponse> Handle(AddRestaurantCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var restaurant = new Restaurant()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    Description = request.Description,
                    Email = request.Email,
                };
                if (request.Image != null)
                {
                    restaurant.SignedUrl = _photoManager.AddPhoto(request.Image);
                }
                var loc = new RestaurantLocation() { Id = Guid.NewGuid().ToString(), Latitude = request.Latitude, Longitude = request.Longitude };
                await _unitOfWork.W_RestaurantLocation.AddAsync(loc);
                await _unitOfWork.W_RestaurantLocation.SaveChangesAsync();
                restaurant.RestaurantLocationId = loc.Id;
                var result = await _unitOfWork.W_Restaurant.AddAsync(restaurant);
                if (result) await _unitOfWork.W_Restaurant.SaveChangesAsync();
                return new() { Success = result };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        
    }
}
