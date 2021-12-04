using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SelfWork.Data
{
    public sealed class IncomeRequest
    {
        public sealed class Service
        {
            [JsonProperty]
            public string Name { get; set; }

            [JsonProperty]
            public decimal Amount { get; set; }

            [JsonProperty]
            public uint Quantity { get; set; }
        }

        internal sealed class Client
        {
            [JsonProperty]
            public string ContactPhone { get; set; }

            [JsonProperty]
            public string DisplayName { get; set; }

            [JsonProperty]
            public string Inn { get; set; }

            [JsonProperty]
            public string IncomeType { get; set; }
        }

        [JsonProperty]
        internal DateTime OperationTime { get; set; }

        [JsonProperty]
        internal DateTime RequestTime { get; set; }

        [JsonProperty]
        internal List<Service> Services { get; set; }

        [JsonProperty]
        internal decimal TotalAmount { get; set; }

        [JsonProperty("client")]
        internal Client ClientInfo { get; set; }

        [JsonProperty]
        internal string PaymentType { get; set; }

        [JsonProperty]
        internal bool IgnoreMaxTotalIncomeRestriction { get; set; }
    }
}
