using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AccaountManager.Classes.Json.Structs
{
    public struct Settings
    {
        [JsonProperty("path")]
        [JsonRequired]
        public string Path { get; set; }

        [JsonProperty("startWithWindows")]
        [JsonRequired]
        public bool StartWithWindows { get; set; }
    }
}
