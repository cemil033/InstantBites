using InstantBites.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Clients.LogOutClient
{
    public class LogOutClientQueryHandler : IRequestHandler<LogOutClientQueryRequest, LogOutClientQueryResponse>
    {
        private readonly SignInManager<Client> _singInManager;

        public LogOutClientQueryHandler(SignInManager<Client> singInManager)
        {
            _singInManager = singInManager;
        }

        public async Task<LogOutClientQueryResponse> Handle(LogOutClientQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _singInManager.SignOutAsync();
                return new() { Success = true };
            }

            catch (Exception)
            {
                throw;
            }
        }    
    }
}
