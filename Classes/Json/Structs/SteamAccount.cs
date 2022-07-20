using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AccaountManager.Classes.Json.Structs
{
    public struct SteamAccount
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }  
    }
}
