using InstantBites.Application.Repositories.ClientOrdersRepository;
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

namespace InstantBites.Application.Features.Commands.Orders.StopSubscribeOrder
{
    public class StopSubscribeOrderCommandHandler : IRequestHandler<StopSubscribeOrderCommandRequest, StopSubscribeOrderCommandResponse>
    {
        private readonly UserManager<Client> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public StopSubscribeOrderCommandHandler(UserManager<Client> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }
        

        public async Task<StopSubscribeOrderCommandResponse> Handle(StopSubscribeOrderCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                var clorder = _unitOfWork.R_ClientOrders.GetAll().FirstOrDefault(e => (e.OrderId == request.OrderId) && (e.ClientId == user.Id));
                if (clorder!=null)
                {
                    await _unitOfWork.W_ClientOrders.RemoveAsync(clorder.Id);
                    await _unitOfWork.W_ClientOrders.SaveChangesAsync();
                    return new() { Success = true };
                }
                return new() { Success = false };
            }
            catch (Exception)
            {
                throw;
            }
            

        }
    }
}
