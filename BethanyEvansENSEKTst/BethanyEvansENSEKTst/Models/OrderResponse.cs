using Newtonsoft.Json;

namespace BethanyEvansENSEKTest.Models
{
    public class OrderResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("energy_id")]
        public int Energy_Id { get; set; }
    }
}
