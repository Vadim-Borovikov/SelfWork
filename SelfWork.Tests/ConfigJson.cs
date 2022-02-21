using Newtonsoft.Json;

namespace SelfWork.Tests;

internal sealed class ConfigJson
{
    [JsonProperty]
    public string? SourceDeviceId { get; set; }

    [JsonProperty]
    public string? RefreshToken { get; set; }
}