using InstantBites.Application.Repositories.ClientOrdersRepository;
using InstantBites.Application.Repositories.ClientLocationRepository;
using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Application.Repositories.RestaurantRepository;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace InstantBites.Application.Features.Commands.Orders.SubscribeOrder
{
    public class SubscribeOrderCommandHandler : IRequestHandler<SubscribeOrderCommandRequest, SubscribeOrderCommandResponse>
    {
        private readonly UserManager<Client> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;        

        public SubscribeOrderCommandHandler(UserManager<Client> userManager, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<SubscribeOrderCommandResponse> Handle(SubscribeOrderCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.R_Order.Get(request.Id);
                var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                var uId = _userManager.GetUserId(_contextAccessor.HttpContext.User);
                if (_unitOfWork.R_ClientOrders.GetAll().Any(e => e.OrderId == order.Id && e.ClientId == uId))
                {
                    return new() { Success = false, IsSubscribed = true };
                }
                var rest = await _unitOfWork.R_Restaurant.Get(order.RestaurantId, false);
                var restloc = await _unitOfWork.R_RestaurantLocation.Get(rest.RestaurantLocationId, false);
                var userloc = await _unitOfWork.R_ClientLocation.Get(user.ClientLocationId, false);
                var shipping = Math.Floor((DistanceTo(restloc.Latitude, restloc.Longitude, userloc.Latitude, userloc.Longitude) * 0.02) * 100) / 100;
                if ((shipping + order.Price) > user.Balance)
                {
                    return new() { Success = false, IsSubscribed = false };
                }
                var om = new ClientOrders()
                {
                    OrderId = order.Id,
                    ClientId = uId,
                    LastSubscribedTime = DateTime.Now,
                    Id = Guid.NewGuid().ToString(),
                    Shipping = shipping
                };
                var res = await _unitOfWork.W_ClientOrders.AddAsync(om);
                if (res) await _unitOfWork.W_ClientOrders.SaveChangesAsync();
                rest.RestaurantLocation = restloc;
                return new() { Success = true,Restaurant=rest ,Order=order};
            }
            catch (Exception)
            {
                throw;
            }
            
        }



        public double DistanceTo(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return dist * 160.9344;
        }
    }
}
