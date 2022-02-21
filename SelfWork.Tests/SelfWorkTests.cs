using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SelfWork.Tests;

[TestClass]
public sealed class SelfWorkTests
{
    [ClassInitialize]
    public static void ClassInitialize(TestContext _)
    {
        _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                            // Create appsettings.json for private settings
                                            .AddJsonFile("appsettings.json")
                                            .Build()
                                            .Get<ConfigJson>();
    }

    [TestMethod]
    public async Task TestGetToken() => await GetTokenAsync();

    [TestMethod]
    public async Task TestPostAndCancelIncomeAsync()
    {
        string token = await GetTokenAsync();

        string receiptId = await DataManager.PostIncomeFromIndividualAsync("Тест", 1000, token);
        Assert.IsFalse(string.IsNullOrEmpty(receiptId));
        Uri uri = DataManager.GetReceiptUri(1, receiptId);
        Assert.IsNotNull(uri);

        bool success = await DataManager.CancelIncome(receiptId, token);
        Assert.IsTrue(success);
    }

    private static async Task<string> GetTokenAsync()
    {
        Assert.IsNotNull(_config.SourceDeviceId);
        Assert.IsNotNull(_config.RefreshToken);
        string token = await DataManager.GetTokenAsync(UserAgent, _config.SourceDeviceId, SourceType, AppVersion,
            _config.RefreshToken);
        Assert.IsFalse(string.IsNullOrEmpty(token));
        return token;
    }

    private const string UserAgent =
        "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Mobile Safari/537.36";
    private const string SourceType = "WEB";
    private const string AppVersion = "1.0.0";

    // ReSharper disable once NullableWarningSuppressionIsUsed
    //   _config initializes in ClassInitialize
    private static ConfigJson _config = null!;
}