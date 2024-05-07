using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Infrastructure.Authentication
{
    public class JwtSetting
    {
        public const string SectionName = "JwtSetting";
        public string? Secret { get; set; } = null;
        public int ExpiryMinutes { get; set; }
        public string? Issuer { get; set; } = null;
        public string? Audience { get; set; } = null;

    }
}
