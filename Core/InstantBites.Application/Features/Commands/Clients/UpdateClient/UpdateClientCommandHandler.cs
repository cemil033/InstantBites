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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Clients.UpdateClient
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommandRequest, UpdateClientCommandResponse>
    {        
        private readonly UserManager<Client> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly PhotoManager _photoManager;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClientCommandHandler(UserManager<Client> userManager, IHttpContextAccessor contextAccessor, IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork, PhotoManager photoManager)
        {            
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _jwtTokenGenerator = jwtTokenGenerator;
            _unitOfWork = unitOfWork;
            _photoManager = photoManager;
        }

        public async Task<UpdateClientCommandResponse> Handle(UpdateClientCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                var curpass = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
                bool emailIsChange = false;
                if (curpass)
                {
                    user.Name = request.Name;
                    user.Surname = request.Surname;
                    user.PhoneNumber = request.PhoneNumber; 
                    user.UserName = request.UserName;
                    if (request.Email != user.Email)
                    {
                        user.EmailConfirmed = false;
                        user.Email = request.Email;
                        emailIsChange = true;
                    }                    
                    user.Balance = request.Balance;
                    user.Active = true;
                    var loc = await _unitOfWork.R_ClientLocation.Get(user.ClientLocationId, false);
                    if (loc.Longitude != request.Longitude || loc.Latitude != request.Latitude)
                    {
                        var loc1 = new ClientLocation() { Id = Guid.NewGuid().ToString(), Latitude = request.Latitude, Longitude = request.Longitude };
                        await _unitOfWork.W_ClientLocation.RemoveAsync(user.ClientLocationId);
                        await _unitOfWork.W_ClientLocation.AddAsync(loc1);
                        await _unitOfWork.W_ClientLocation.SaveChangesAsync();
                        user.ClientLocationId = loc1.Id;
                    }
                    if (request.Image != null)
                    {
                        user.SignedUrl = _photoManager.AddPhoto(request.Image);
                    }

                    user.UpdatedTime = DateTime.UtcNow;
                    if (request.Password != null)
                    {
                        var resultPass = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.Password);
                        if (resultPass.Succeeded)
                        {
                            var resultUpdate = await _userManager.UpdateAsync(user);
                            if (resultUpdate.Succeeded)
                            {
                                var token = _jwtTokenGenerator.GenerateToken(user.Id, user.UserName);
                                return new()
                                {
                                    Title = "Update",
                                    Token = token,
                                    Success = true,
                                    IsChangeEmail = emailIsChange
                                };
                            }
                            else
                            {
                                var response = new UpdateClientCommandResponse()
                                {
                                    Success = false,
                                    Title = "Update"
                                };
                                foreach (var error in resultUpdate.Errors)
                                {
                                    response.Results.Add(error.Code, error.Description);
                                }
                                return response;
                            }
                        }
                        else
                        {
                            var response = new UpdateClientCommandResponse()
                            {
                                Success = false,
                                Title = "Update"
                            };
                            foreach (var error in resultPass.Errors)
                            {
                                response.Results.Add(error.Code, error.Description);
                            }
                            return response;
                        }
                    }



                    var resultUpdate1 = await _userManager.UpdateAsync(user);
                    if (resultUpdate1.Succeeded)
                    {
                        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.UserName);
                        return new()
                        {
                            Title = "Update",
                            Token = token,
                            Success = true,
                            IsChangeEmail = emailIsChange
                        };
                    }
                    else
                    {
                        var response = new UpdateClientCommandResponse()
                        {
                            Success = false,
                            Title = "Update"
                        };
                        foreach (var error in resultUpdate1.Errors)
                        {
                            response.Results.Add(error.Code, error.Description);
                        }
                        return response;
                    }
                }
                else
                {
                    var response = new UpdateClientCommandResponse()
                    {
                        Success = false,
                        Title = "Update"
                    };
                    response.Results = new();
                    response.Results.Add("CurrentPassword", "Current Password is not correct");
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
