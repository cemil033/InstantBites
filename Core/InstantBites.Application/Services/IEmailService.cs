using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Domain.Services
{
    public interface IEmailService
    {
        public  Task<HttpStatusCode> Send(string email, bool confmail, IUrlHelper url);
        public Task<HttpStatusCode> SendMessage(string email, string message, IUrlHelper url);
    }
}
