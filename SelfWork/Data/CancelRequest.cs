using System;
using Newtonsoft.Json;

namespace SelfWork.Data;

internal sealed class CancelRequest
{
    [JsonProperty]
    public string? ReceiptUuid { get; set; }

    [JsonProperty]
    public string? Comment { get; set; }

    [JsonProperty]
    public DateTime? OperationTime { get; set; }

    [JsonProperty]
    public DateTime? RequestTime { get; set; }
}