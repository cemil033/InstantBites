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

namespace InstantBites.Application.Features.Queries.Clients.GetClientNotifications
{
    public class GetClientNotificationsQueryHandler : IRequestHandler<GetClientNotificationsQueryRequest, GetClientNotificationsQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;        
        private readonly UserManager<Client> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;       

        public GetClientNotificationsQueryHandler(IUnitOfWork unitOfWork, UserManager<Client> userManager, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<GetClientNotificationsQueryResponse> Handle(GetClientNotificationsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                var notfs = _unitOfWork.R_Notification.GetWhere(e => e.ClientId == user.Id);
                return new() { Notifications = notfs.ToList() };
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
