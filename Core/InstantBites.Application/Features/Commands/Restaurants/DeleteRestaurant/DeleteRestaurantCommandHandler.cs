using InstantBites.Application.Repositories.RestaurantLocationRepository;
using InstantBites.Application.Repositories.RestaurantRepository;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Restaurants.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommandRequest, DeleteRestaurantCommandResponse>
    {        
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRestaurantCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
               

        public async Task<DeleteRestaurantCommandResponse> Handle(DeleteRestaurantCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var fname = (await _unitOfWork.R_Restaurant.Get(request.Id, false)).SavedFileName;
                var locid = (await _unitOfWork.R_Restaurant.Get(request.Id, false)).RestaurantLocationId;
                var result = await _unitOfWork.W_Restaurant.RemoveAsync(request.Id);
                if (result)
                {
                    if (string.IsNullOrWhiteSpace(fname))
                        await _unitOfWork.W_Restaurant.SaveChangesAsync();
                    else
                    {                        
                        await _unitOfWork.W_RestaurantLocation.RemoveAsync(locid);
                        await _unitOfWork.W_Restaurant.SaveChangesAsync();
                    }
                }
                return new() { Success = result };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
