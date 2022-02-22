using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GryphonUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SelfWork.Data;

namespace SelfWork;

internal static class Provider
{
    public static Task<TokenResponse> GetTokenAsync(string userAgent, string sourceDeviceId, string sourceType,
        string appVersion, string refreshToken)
    {
        TokenRequest.Device.MetaDetailsInfo metaDetails = new() { UserAgent = userAgent };
        TokenRequest.Device deviceInfo = new()
        {
            SourceDeviceId = sourceDeviceId,
            SourceType = sourceType,
            AppVersion = appVersion,
            MetaDetails = metaDetails
        };
        TokenRequest tokenRequestObj = new()
        {
            DeviceInfo = deviceInfo,
            RefreshToken = refreshToken
        };

        return RestHelper.CallPostMethodAsync<TokenRequest, TokenResponse>(ApiProvider, GetTokenMethod,
            obj: tokenRequestObj);
    }

    public static Task<IncomeResponse> PostIncomeFromIndividualAsync(DateTime operationTime, DateTime requestTime,
        IEnumerable<IncomeRequest.Service> services, decimal totalAmount, string token)
    {
        IncomeRequest.ClientInfo client = new();
        IncomeRequest incomeRequestObj = new()
        {
            OperationTime = operationTime,
            RequestTime = requestTime,
            Services = services.Select(s => (IncomeRequest.Service?) s).ToList(),
            TotalAmount = totalAmount,
            Client = client,
            PaymentType = PaymentType
        };

        return CallPostMethodAsync<IncomeRequest, IncomeResponse>(PostIncomeMethod, token, incomeRequestObj);
    }

    public static Task<CancelResponse> CancelIncomeAsync(string receiptUuid, string comment, DateTime operationTime,
        DateTime requestTime, string token)
    {
        CancelRequest cancelRequestObj = new()
        {
            ReceiptUuid = receiptUuid,
            Comment = comment,
            OperationTime = operationTime,
            RequestTime = requestTime
        };

        return CallPostMethodAsync<CancelRequest, CancelResponse>(CancelMethod, token, cancelRequestObj);
    }

    private static Task<TResponse> CallPostMethodAsync<TRequest, TResponse>(string method, string token, TRequest obj)
        where TRequest : class
    {
        string headerValue = $"Bearer {token}";
        return RestHelper.CallPostMethodAsync<TRequest, TResponse>(ApiProvider, method, HeaderName, headerValue, obj,
            Settings);
    }

    internal const string ApiProvider = "https://lknpd.nalog.ru/api/v1/";

    private const string GetTokenMethod = "auth/token";
    private const string PostIncomeMethod = "income";
    private const string CancelMethod = "cancel";
    private const string PaymentType = "CASH";
    private const string HeaderName = "Authorization";

    private static readonly JsonSerializerSettings Settings = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        DateFormatString = "yyyy-MM-ddTHH\\:mm\\:ss.fffzzz"
    };
}