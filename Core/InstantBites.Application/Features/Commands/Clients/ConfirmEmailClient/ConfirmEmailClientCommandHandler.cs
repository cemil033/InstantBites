using InstantBites.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Clients.ConfirmEmailClient
{
    public class ConfirmEmailClientCommandHandler : IRequestHandler<ConfirmEmailClientCommandRequest, ConfirmEmailClientCommandResponse>
    {
        private readonly UserManager<Client> _userManager;

        public ConfirmEmailClientCommandHandler(UserManager<Client> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ConfirmEmailClientCommandResponse> Handle(ConfirmEmailClientCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                user.EmailConfirmed = true;
                var res = await _userManager.UpdateAsync(user);
                return new() { Success = res.Succeeded };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
