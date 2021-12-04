using Newtonsoft.Json;

namespace SelfWork.Data
{
    public sealed class IncomeResult
    {
        [JsonProperty]
        public string ApprovedReceiptUuid { get; set; }

        [JsonProperty]
        public string ApprovedReceiptUrl { get; set; }
    }
}
