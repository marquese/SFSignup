using Newtonsoft.Json;
using System.Collections.Generic;

namespace SFSignup
{
    public class RaidZone
    {
        public string Name { get; set; }
        [JsonProperty(PropertyName ="description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        public List<Boss> Bosses;

    }
}