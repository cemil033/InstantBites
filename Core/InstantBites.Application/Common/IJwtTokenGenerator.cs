using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Common
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(string userId, string Name);
    }
}
