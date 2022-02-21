using Newtonsoft.Json;

namespace SelfWork.Data;

internal sealed class TokenRequest
{
    public sealed class Device
    {
        public sealed class MetaDetailsInfo
        {
            [JsonProperty]
            public string? UserAgent { get; set; }
        }

        [JsonProperty]
        public string? SourceDeviceId { get; set; }

        [JsonProperty]
        public string? SourceType { get; set; }

        [JsonProperty]
        public string? AppVersion { get; set; }

        [JsonProperty]
        public MetaDetailsInfo? MetaDetails { get; set; }
    }

    [JsonProperty]
    public Device? DeviceInfo { get; set; }

    [JsonProperty]
    public string? RefreshToken { get; set; }
}