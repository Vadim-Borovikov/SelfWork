using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SelfWork.Data;

namespace SelfWork
{
    internal static class Provider
    {
        public static Task<TokenResult> GetTokenAsync(string userAgent, string sourceDeviceId, string sourceType,
            string appVersion, string refreshToken)
        {
            var metaDetails = new TokenRequest.Device.MetaDetails { UserAgent = userAgent };
            var deviceInfo = new TokenRequest.Device
            {
                SourceDeviceId = sourceDeviceId,
                SourceType = sourceType,
                AppVersion = appVersion,
                MetaDetailsInfo = metaDetails
            };
            var tokenRequestDto = new TokenRequest
            {
                DeviceInfo = deviceInfo,
                RefreshToken = refreshToken
            };

            return RestHelper.CallPostMethodAsync<TokenResult>(ApiProvider, GetTokenMethod, tokenRequestDto, Settings);
        }

        public static Task<IncomeResult> PostIncomeFromIndividualAsync(DateTime operationTime, DateTime requestTime,
            List<IncomeRequest.Service> services, decimal totalAmount, string token)
        {
            var client = new IncomeRequest.Client();
            var incomeRequestDto = new IncomeRequest
            {
                OperationTime = operationTime,
                RequestTime = requestTime,
                Services = services,
                TotalAmount = totalAmount,
                ClientInfo = client,
                PaymentType = PaymentType
            };

            return RestHelper.CallPostMethodAsync<IncomeResult>(ApiProvider, PostIncomeMethod, incomeRequestDto, Settings,
                token);
        }

        public static Task<CancelResult> CancelIncomeAsync(string receiptUuid, string comment, DateTime operationTime,
            DateTime requestTime, string token)
        {
            var cancelRequestDto = new CancelRequest
            {
                ReceiptUuid = receiptUuid,
                Comment = comment,
                OperationTime = operationTime,
                RequestTime = requestTime
            };

            return RestHelper.CallPostMethodAsync<CancelResult>(ApiProvider, CancelMethod, cancelRequestDto, Settings,
                token);
        }

        internal const string ApiProvider = "https://lknpd.nalog.ru/api/v1/";

        private const string GetTokenMethod = "auth/token";
        private const string PostIncomeMethod = "income";
        private const string CancelMethod = "cancel";
        private const string PaymentType = "CASH";

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatString = "yyyy-MM-ddTHH\\:mm\\:ss.fffzzz"
        };
    }
}
