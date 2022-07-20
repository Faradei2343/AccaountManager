using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AccaountManager.Classes.Json.Structs
{
    public struct SteamPathInfoStruct
    {
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
