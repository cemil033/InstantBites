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

namespace InstantBites.Application.Features.Queries.Clients.GetClientOrders
{
    public class ClientGetOrdersQueryHandler : IRequestHandler<ClientGetOrdersQueryRequest, ClientGetOrdersQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Client> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public ClientGetOrdersQueryHandler(IUnitOfWork unitOfWork, UserManager<Client> userManager, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<ClientGetOrdersQueryResponse> Handle(ClientGetOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                var clor = _unitOfWork.R_ClientOrders.GetWhere(e => e.ClientId == user.Id,false).ToList();
                var orders = _unitOfWork.R_Order.GetAll(false).ToList();
                if(clor.Count > 0)
                {
                    foreach (var item in clor)
                    {
                        item.Order = orders.FirstOrDefault(e => e.Id == item.OrderId);
                    }
                    return new() { Orders = clor, Success = true };
                }

                return new() { Orders = new(), Success = true };
            }
            catch (Exception)
            {
                throw;
            }   
        }
    }
}
