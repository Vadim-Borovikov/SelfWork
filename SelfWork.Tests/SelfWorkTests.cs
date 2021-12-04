using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelfWork.Data;

namespace SelfWork.Tests
{
    [TestClass]
    public sealed class SelfWorkTests
    {
        [TestMethod]
        public void TestGetToken() { GetToken(); }

        [TestMethod]
        public void TestPostIncome()
        {
            string token = GetToken();
            IncomeResult result = DataManager.PostIncome("Тест", 1000, token);
            Assert.IsNotNull(result);
        }

        private static string GetToken()
        {
            Configuration config = GetConfig();
            string token = DataManager.GetToken(UserAgent, config.SourceDeviceId, SourceType, AppVersion, config.RefreshToken);
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
