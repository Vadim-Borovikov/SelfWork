using Newtonsoft.Json;

namespace SelfWork.Data
{
    internal sealed class TokenResult
    {
        [JsonProperty]
        public string Token { get; set; }
    }
}
