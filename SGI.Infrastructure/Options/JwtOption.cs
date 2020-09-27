using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.Infrastructure.Options
{
    public class JwtOption
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthEndpoint { get; set; }
        public string AdminClientUsername { get; set; }
        public string AdminClientPassword { get; set; }
        public string BaseAddress { get; set; }
    }
}
