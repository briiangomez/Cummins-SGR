using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGI_WebApi_Pauny.Models
{
    public class RefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
