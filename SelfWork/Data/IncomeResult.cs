using Newtonsoft.Json;

namespace SelfWork.Data
{
    internal sealed class IncomeResult
    {
        [JsonProperty]
        public string ApprovedReceiptUuid { get; set; }
    }
}
