using Newtonsoft.Json;

namespace BethanyEvansENSEKTest.Models
{
    public class Buy
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
