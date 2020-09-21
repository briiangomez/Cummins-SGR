using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGI.Infrastructure.Entities
{
    public class ErrorResponse
    {
        [JsonProperty("error")]
        public string Message { get; set; }

        [JsonProperty("error_description")]
        public string Description { get; set; }

        public ErrorResponse()
        {

        }
    }
}
