﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Clients.LoginClient
{
    public class LoginClientCommandResponse
    {
        public bool Success { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Token { get; set; }
    }
}
