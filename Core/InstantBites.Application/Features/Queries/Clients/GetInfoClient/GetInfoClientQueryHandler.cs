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

namespace InstantBites.Application.Features.Queries.Clients.GetInfoClient
{
    public class GetInfoClientQueryHandler : IRequestHandler<GetInfoClientQueryRequest, GetInfoClientQueryResponse>
    {        
        private readonly UserManager<Client> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;        
        private readonly IUnitOfWork _unitOfWork;

        public GetInfoClientQueryHandler(UserManager<Client> userManager, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetInfoClientQueryResponse> Handle(GetInfoClientQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                var loc = await _unitOfWork.R_ClientLocation.Get(e => e.Id == client.ClientLocationId,false);
                if (client != null)
                {
                    if (loc != null)
                    {
                        client.ClientLocation = loc;
                    }
                    return new() { Client = client, Success = true };
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
