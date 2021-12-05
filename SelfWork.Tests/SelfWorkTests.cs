using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SelfWork.Tests
{
    [TestClass]
    public sealed class SelfWorkTests
    {
        [TestMethod]
        public async Task TestGetToken() => await GetTokenAsync();

        [TestMethod("Post — and then CANCEL income MANUALY!")]
        public async Task TestPostIncomeAsync()
        {
            string token = await GetTokenAsync();
            string receiptId = await DataManager.PostIncomeFromIndividualAsync("Тест", 1000, token);
            Assert.IsFalse(string.IsNullOrEmpty(receiptId));
            Uri uri = DataManager.GetReceiptUri(1, receiptId);
            Assert.IsNotNull(uri);
        }

        private static async Task<string> GetTokenAsync()
        {
            Configuration config = GetConfig();
            string token =
                await DataManager.GetTokenAsync(UserAgent, config.SourceDeviceId, SourceType, AppVersion, config.RefreshToken);
            Assert.IsFalse(string.IsNullOrEmpty(token));
            return token;
        }

        private static Configuration GetConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json") // Create appsettings.json for private settings
                .Build()
                .Get<Configuration>();
        }

        private const string UserAgent =
            "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Mobile Safari/537.36";
        private const string SourceType = "WEB";
        private const string AppVersion = "1.0.0";
    }
}
