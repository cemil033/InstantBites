using InstantBites.Application.Features.Commands.Restaurants.AddRestaurant;
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
using System.Xml;

namespace InstantBites.Application.Features.Commands.Restaurants.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommandRequest, UpdateRestaurantCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PhotoManager _photoManager;

        public UpdateRestaurantCommandHandler(IUnitOfWork unitOfWork, PhotoManager photoManager)
        {
            _unitOfWork = unitOfWork;
            _photoManager = photoManager;
        }        

        public async Task<UpdateRestaurantCommandResponse> Handle(UpdateRestaurantCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var restaurant = await _unitOfWork.R_Restaurant.Get(request.Id, false);
                restaurant.Name = request.Name;
                restaurant.Description = request.Description;
                restaurant.Email=request.Email;
                var loc = await _unitOfWork.R_RestaurantLocation.Get(restaurant.RestaurantLocationId, false);
                if (loc.Longitude != request.Longitude || loc.Latitude != request.Latitude)
                {
                    loc.Longitude=request.Longitude;
                    loc.Latitude=request.Latitude;
                    await _unitOfWork.W_RestaurantLocation.Update(loc);                    
                }
                if (request.Image != null)
                {
                    restaurant.SignedUrl = _photoManager.AddPhoto(request.Image);
                }
                var result = await  _unitOfWork.W_Restaurant.Update(restaurant);                
                if (result)
                   await _unitOfWork.W_Restaurant.SaveChangesAsync();                
                return new() { Success = result };
            }
            catch (Exception)
            {
                throw;
            }
            
        }        
    }
}
