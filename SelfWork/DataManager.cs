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
        public enum IncomeType
        {
            FromIndividual,
            FromLegalEntity,
            FromForeignAgency
        }

        public static async Task<string> GetTokenAsync(string userAgent, string sourceDeviceId, string sourceType,
            string appVersion, string refreshToken)
        {
            TokenResult result = await Provider.GetTokenAsync(userAgent, sourceDeviceId, sourceType, appVersion, refreshToken);
            return result.Token;
        }

        public static Task<IncomeResult> PostIncomeAsync(string name, decimal amount, string token,
            IncomeType? incomeType = null, DateTime? operationTime = null)
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

            return Provider.PostIncomeAsync(operationTime.Value, now, services, amount, token, GetValue(incomeType));
        }

        private static string GetValue(IncomeType? incomeType)
        {
            switch (incomeType)
            {
                case IncomeType.FromIndividual: return FromIndividualValue;
                case IncomeType.FromLegalEntity: return FromLegalEntityValue;
                case IncomeType.FromForeignAgency: return FromForeignAgencyValue;
                case null: return null;
                default: throw new ArgumentOutOfRangeException(nameof(incomeType), incomeType, null);
            }
        }

        private const string FromIndividualValue = "FROM_INDIVIDUAL";
        private const string FromLegalEntityValue = "FROM_LEGAL_ENTITY";
        private const string FromForeignAgencyValue = "FROM_FOREIGN_AGENCY";
    }
}
