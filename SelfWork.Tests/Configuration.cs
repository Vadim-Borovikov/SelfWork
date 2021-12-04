using Newtonsoft.Json;

namespace SelfWork.Tests
{
    internal sealed class Configuration
    {
        [JsonProperty]
        public string SourceDeviceId { get; set; }

        [JsonProperty]
        public string RefreshToken { get; set; }
    }
}
