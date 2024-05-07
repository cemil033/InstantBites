using InstantBites.Application.Repositories.NotificationRepository;
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

namespace InstantBites.Application.Features.Commands.Clients.ClearClientNotifications
{
    public class ClearClientNotificationsCommandHandler : IRequestHandler<ClearClientNotificationsCommandRequest, ClearClientNotificationsCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Client> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public ClearClientNotificationsCommandHandler(IUnitOfWork unitOfWork, UserManager<Client> userManager, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<ClearClientNotificationsCommandResponse> Handle(ClearClientNotificationsCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                foreach (var item in _unitOfWork.R_Notification.GetAll())
                {
                    if (item.ClientId == client.Id)
                    {
                        _unitOfWork.W_Notification.Remove(item);
                    }
                }
                await _unitOfWork.W_Notification.SaveChangesAsync();
                return new() { Success = true };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
