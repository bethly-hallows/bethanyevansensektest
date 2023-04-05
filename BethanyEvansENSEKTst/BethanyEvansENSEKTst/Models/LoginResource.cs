using Newtonsoft.Json;

namespace BethanyEvansENSEKTest.Models
{
    public class LoginResource
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
