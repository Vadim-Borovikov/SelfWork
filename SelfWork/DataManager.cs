using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SelfWork.Data;

namespace SelfWork
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class DataManager
    {
        public static async Task<string> GetTokenAsync(string userAgent, string sourceDeviceId, string sourceType,
            string appVersion, string refreshToken)
        {
            TokenResult result = await Provider.GetTokenAsync(userAgent, sourceDeviceId, sourceType, appVersion, refreshToken);
            return result.Token;
        }

        public static Task<IncomeResult> PostIncomeAsync(string name, decimal amount, string token,
            DateTime? operationTime = null)
        {
            var service = new IncomeRequest.Service
            {
                Amount = amount,
                Name = name,
                Quantity = 1
            };
            var services = new List<IncomeRequest.Service> { service };

            DateTime now = DateTime.Now;
            if (!operationTime.HasValue)
            {
                operationTime = now;
            }

            return Provider.PostIncomeFromIndividualAsync(operationTime.Value, now, services, amount, token);
        }
    }
}
