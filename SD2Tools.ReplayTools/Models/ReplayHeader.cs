using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SD2Tools.ReplayTools.Utils;

namespace SD2Tools.ReplayTools.Models
{
    [JsonConverter(typeof(ReplayHeaderConverter))]
    public class ReplayHeader
    {
        public Game Game { get; set; }
        public List<Player> Players { get; set; }

        public ReplayHeader()
        {
            Players = new List<Player>();
        }
    }
}
