using Newtonsoft.Json;

namespace BethanyEvansENSEKTest.Models
{
    public class OrderResource
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; }

        [JsonProperty("energy_id")]
        public int Energy_Id { get; set; }
    }
}
