using Newtonsoft.Json;

namespace BethanyEvansENSEKTest.Models
{
    public class LoginResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
