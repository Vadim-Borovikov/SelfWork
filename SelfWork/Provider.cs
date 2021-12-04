using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SelfWork.Data;

namespace SelfWork
{
    internal static class Provider
    {
        public static TokenResult GetToken(string userAgent, string sourceDeviceId, string sourceType,
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

            return RestHelper.CallPostMethod<TokenResult>(ApiProvider, GetTokenMethod, tokenRequestDto, Settings);
        }

        public static IncomeResult PostIncome(DateTime operationTime, DateTime requestTime,
            List<IncomeRequest.Service> services, decimal totalAmount, string token, string incomeType)
        {
            var client = new IncomeRequest.Client { IncomeType = incomeType };
            var incomeRequestDto = new IncomeRequest
            {
                OperationTime = operationTime,
                RequestTime = requestTime,
                Services = services,
                TotalAmount = totalAmount,
                ClientInfo = client,
                PaymentType = PaymentType
            };

            return RestHelper.CallPostMethod<IncomeResult>(ApiProvider, PostIncomeMethod, incomeRequestDto, Settings,
                token);
        }

        private const string ApiProvider = "https://lknpd.nalog.ru/api/v1/";
        private const string GetTokenMethod = "auth/token";
        private const string PostIncomeMethod = "income";
        private const string PaymentType = "CASH";

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatString = "yyyy-MM-ddTHH\\:mm\\:ss.fffzzz"
        };
    }
}
