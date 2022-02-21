using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GryphonUtilities;
using SelfWork.Data;

namespace SelfWork;

public static class DataManager
{
    public static async Task<string> GetTokenAsync(string userAgent, string sourceDeviceId, string sourceType,
        string appVersion, string refreshToken)
    {
        TokenResponse result =
            await Provider.GetTokenAsync(userAgent, sourceDeviceId, sourceType, appVersion, refreshToken);
        return result.Token.GetValue(nameof(result.Token));
    }

    public static async Task<string> PostIncomeFromIndividualAsync(string name, decimal amount, string token,
        DateTime? operationTime = null)
    {
        IncomeRequest.Service service = new()
        {
            Amount = amount,
            Name = name,
            Quantity = 1
        };
        List<IncomeRequest.Service> services = new() { service };

        DateTime now = DateTime.Now;
        operationTime ??= now;

        IncomeResponse result =
            await Provider.PostIncomeFromIndividualAsync(operationTime.Value, now, services, amount, token);
        return result.ApprovedReceiptUuid.GetValue(nameof(result.ApprovedReceiptUuid));
    }

    public static async Task<bool> CancelIncome(string uuid, string token)
    {
        DateTime now = DateTime.Now;
        CancelResponse result = await Provider.CancelIncomeAsync(uuid, CancellationComment, now, now, token);
        return result.IncomeInfo?.ApprovedReceiptUuid == uuid;
    }

    public static Uri GetReceiptUri(long payerId, string receiptId)
    {
        string url = string.Format(TaxReceiptUrlFormat, Provider.ApiProvider, payerId, receiptId);
        return new Uri(url);
    }

    private const string TaxReceiptUrlFormat = "{0}receipt/{1}/{2}/print";
    private const string CancellationComment = "Чек сформирован ошибочно";
}