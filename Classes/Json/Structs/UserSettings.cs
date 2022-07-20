using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AccaountManager.Classes.Json.Structs
{
    public struct UserSettings
    {
        [JsonProperty("SteamPath")]
        public string SteamPath { get; set; }

        [JsonProperty("StartWithWindows")]
        public bool StartWithWindows { get; set; }
    }
}
