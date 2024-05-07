using InstantBites.Application.Repositories.ClientRepository;
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

namespace InstantBites.Application.Features.Commands.Clients.DeactivateClient
{
    public class DeactivateClientCommandHandler : IRequestHandler<DeactivateClientCommandRequest, DeactivateClientCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<Client> _signInManager;
        private readonly UserManager<Client> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public DeactivateClientCommandHandler(IUnitOfWork unitOfWork, SignInManager<Client> signInManager, UserManager<Client> userManager, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<DeactivateClientCommandResponse> Handle(DeactivateClientCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                client.Active = false;
                await _unitOfWork.W_Client.Update(client);
                await _unitOfWork.W_Client.SaveChangesAsync();
                await _signInManager.SignOutAsync();
                return new();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
