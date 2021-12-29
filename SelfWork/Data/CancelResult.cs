using Newtonsoft.Json;

namespace SelfWork.Data
{
    internal sealed class CancelResult
    {
        public sealed class Income
        {
            [JsonProperty]
            public string ApprovedReceiptUuid { get; set; }
        }

        [JsonProperty]
        public Income IncomeInfo { get; set; }
    }
}
