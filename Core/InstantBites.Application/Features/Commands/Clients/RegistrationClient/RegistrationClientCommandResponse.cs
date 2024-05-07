using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Clients.RegistrationClient
{
    public class RegistrationClientCommandResponse
    {
        public bool Success { get; set; }
        public string Title { get; set; }
        public string Conflink { get; set; }
        public Dictionary<string, string> Results { get; set; }
        public string Token { get; set; }
    }
}
