using Newtonsoft.Json;

namespace SelfWork.Data;

internal sealed class IncomeResponse
{
    [JsonProperty]
    public string? ApprovedReceiptUuid { get; set; }
}