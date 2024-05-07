using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Infrastructure.Services
{
    public class SendGridOption
    {
        public const string SectionName = "SendGridOption";
        public string ApiKey { get; set; }
        public string EmailFrom { get; set; }
        public string NameFrom { get; set; }
    }
}
