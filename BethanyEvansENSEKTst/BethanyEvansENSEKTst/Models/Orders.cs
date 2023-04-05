using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BethanyEvansENSEKTest.Models
{
    public class Orders
    {
        [JsonProperty("fuel")]
        public string Fuel { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

    }
}
