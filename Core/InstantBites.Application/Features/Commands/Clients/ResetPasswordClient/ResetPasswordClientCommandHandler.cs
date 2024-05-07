using InstantBites.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Clients.ResetPasswordClient
{
    public class ResetPasswordClientCommandHandler : IRequestHandler<ResetPasswordClientCommandRequest, ResetPasswordClientCommandResponse>
    {
        private readonly UserManager<Client> _userManager;

        public ResetPasswordClientCommandHandler(UserManager<Client> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResetPasswordClientCommandResponse> Handle(ResetPasswordClientCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                var cpass = await _userManager.GeneratePasswordResetTokenAsync(user);
                if (user != null)
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, request.Password);
                    await _userManager.UpdateAsync(user);
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
