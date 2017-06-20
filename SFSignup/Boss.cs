using Newtonsoft.Json;

namespace SFSignup
{
    public class Boss
    {
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }


    }
}