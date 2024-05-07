using EASendMail;
using InstantBites.Application.Common;
using InstantBites.Application.Repositories.ClientLocationRepository;
using InstantBites.Application.Services;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using InstantBites.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace InstantBites.Application.Features.Commands.Clients.RegistrationClient
{
    public class RegistrationClientCommandHandler : IRequestHandler<RegistrationClientCommandRequest, RegistrationClientCommandResponse>
    {
        private readonly SignInManager<Client> _signInManager;
        private readonly UserManager<Client> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ControllerContext _controllerContext;
        private readonly PhotoManager _photoManager;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationClientCommandHandler(SignInManager<Client> signInManager, UserManager<Client> userManager, IHttpContextAccessor contextAccessor, IJwtTokenGenerator jwtTokenGenerator, PhotoManager photoManager, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _jwtTokenGenerator = jwtTokenGenerator;
            _photoManager = photoManager;
            _unitOfWork = unitOfWork;            
        }

        public async Task<RegistrationClientCommandResponse> Handle(RegistrationClientCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new Client
                {
                    UserName = request.UserName,
                    Name = request.Name,
                    Surname = request.Surname,
                    Balance = request.Balance,
                    Active = true,
                    Id = Guid.NewGuid().ToString(),
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                };
                if (request.Image != null)
                {
                    user.SignedUrl = _photoManager.AddPhoto(request.Image);
                }
                var loc = new ClientLocation() { Id = Guid.NewGuid().ToString(), Latitude = request.Latitude, Longitude = request.Longitude };
                await _unitOfWork.W_ClientLocation.AddAsync(loc);
                await _unitOfWork.W_ClientLocation.SaveChangesAsync();
                user.ClientLocationId = loc.Id;
                user.EmailConfirmed = false;
                var result = await _userManager.CreateAsync(user, request.Password);
                var res2 = await _userManager.AddToRoleAsync(user, "Client");
                var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Name);
                if (result.Succeeded)
                {       
                    return new()
                    {                        
                        Token = token,
                        Success = true,
                        Title = "Registration"
                    };
                }
                else
                {
                    var response = new RegistrationClientCommandResponse()
                    {
                        Title = "Registration",
                        Success = false
                    };
                    foreach (var error in result.Errors)
                    {
                        response.Results.Add(error.Code, error.Description);
                    }
                    return response;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }       
        
       
    }
}
