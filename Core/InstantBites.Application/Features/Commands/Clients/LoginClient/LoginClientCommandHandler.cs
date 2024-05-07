using InstantBites.Application.Common;
using InstantBites.Application.Repositories.ClientRepository;
using InstantBites.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Clients.LoginClient
{
    public class LoginClientCommandHandler : IRequestHandler<LoginClientCommandRequest, LoginClientCommandResponse>
    {        
        private readonly SignInManager<Client> _signInManager;
        private readonly UserManager<Client> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginClientCommandHandler(SignInManager<Client> signInManager, UserManager<Client> userManager, IHttpContextAccessor contextAccessor, IJwtTokenGenerator jwtTokenGenerator)
        {            
            _signInManager = signInManager;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginClientCommandResponse> Handle(LoginClientCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, request.Password))
                    {
                        if (!user.EmailConfirmed)
                            return new() { Success = false, Key = "Email", Value = "Email is not confirmed" };
                        if (!user.Active)
                            return new() { Success = false, Key = "Active", Value = "Your account is deactive" };
                        await _signInManager.SignInAsync(user, true);

                        return new() { Success = true, Key = "Login", Value = "Success", Token = _jwtTokenGenerator.GenerateToken(user.Id, user.Name) };

                    }

                }

                return new() { Success = false, Key = "Login", Value = "Email or Password incorrect" };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
