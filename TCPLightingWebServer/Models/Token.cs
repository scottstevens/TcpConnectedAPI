using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCPConnectedAPI.Models
{
    public class Token
    {
        public string Value { get; set; }
        [JsonProperty(Required = Newtonsoft.Json.Required.AllowNull)]
        public bool? Success { get; set; }
    }
}