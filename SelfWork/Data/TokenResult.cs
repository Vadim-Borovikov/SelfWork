using Newtonsoft.Json;

namespace SelfWork.Data
{
    public sealed class TokenResult
    {
        [JsonProperty]
        public string Token { get; set; }
    }
}
