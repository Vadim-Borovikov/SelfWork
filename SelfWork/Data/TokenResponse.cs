using Newtonsoft.Json;

namespace SelfWork.Data;

internal sealed class TokenResponse
{
    [JsonProperty]
    public string? Token { get; set; }
}