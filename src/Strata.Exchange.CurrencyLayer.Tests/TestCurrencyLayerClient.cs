using Moq;
using NUnit.Framework;
using Strata.Exchange.CurrencyLayer.Client;
using Strata.Exchange.CurrencyLayer.Client.CurrencyLayer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using System.IO;
using IdentityModel.Client;

namespace Strata.Exchange.CurrencyLayer.Tests
{
    [TestFixture]
    public class TestCurrencyLayer_Client
    {

        private ICurrencyLayerClient currencyClient;
        [SetUp]
        public void Setup()
        {
            var client = new HttpClient();
            var httpClientMock = new Mock<IHttpClientFactory>();

            httpClientMock.Setup(h => h.CreateClient(It.IsAny<string>())).Returns(client);
            currencyClient = new CurrencyLayerClient(httpClientMock.Object);
        }

        [Test]
        public async Task Test_GetSupportedCurrenciesAsync()
        {
            var resp = await this.currencyClient.GetSupportedCurrenciesAsync("ACCESS_KEY");     
            var expected = new SupportedCurrencies()
            {
                Currencies = new Dictionary<string, string>()
                {
                    {"AED","United Arab Emirates Dirham" },
                    { "AFN", "Afghan Afghani"}
                }
            };
            Assert.AreEqual(resp.Currencies["AED"], expected.Currencies["AED"]);
        }

        [Test]
        public async Task Test_GetSupportedCurrenciesAsync_Mock()
        {
            var res = new Mock<ICurrencyLayerClient>();
            var mockData = JsonConvert.DeserializeObject<SupportedCurrencies>(File.ReadAllText(@".\MockData\supportedCurrencies.json"));
            res.Setup(h => h.GetSupportedCurrenciesAsync(It.IsAny<string>())).Returns(Task.FromResult(mockData));

            var expected = new SupportedCurrencies()
            {
                Currencies = new Dictionary<string, string>()
                {
                    {"AED","United Arab Emirates Dirham" },
                    { "AFN", "Afghan Afghani"}
                }
            };

            var actual = await res.Object.GetSupportedCurrenciesAsync("jsafda");
            Assert.AreEqual(expected.Currencies["AED"], actual.Currencies["AED"]);

        }

        [Test]
        public async Task Test_GetSupportedCurrencies_InvalidRequestAsync()
        {

            var resp = await this.currencyClient.GetSupportedCurrenciesAsync("0abc1d892b869d7fa2f528a05984eb9");

            var expected = new SupportedCurrencies()
            {
                Success = false,
                Error = new Error()
                {
                   Type = "invalid_access_key",
                   Code = 101,
                   Info = "You have not supplied a valid API Access Key. [Technical Support: support@apilayer.com]"
                }
            };

            resp.Should().BeEquivalentTo<SupportedCurrencies>(expected);
        }

        [Test]
        public async Task Test_GetSupportedCurrencies_Authorization()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:8471");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }
        }

        [Test]
        public async Task Test_GetForexLiveDataAsync()
        {
            var res = new Mock<ICurrencyLayerClient>();
            var mockData = JsonConvert.DeserializeObject<ForexLiveData>(File.ReadAllText(@".\MockData\forexLiveData.json"));
            res.Setup(h => h.GetLiveForexData(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(mockData));

            var expected = new ForexLiveData()
            {
                Quotes = new Dictionary<string, double>()
                {
                    {"USDGBP",0.739925 }
                }
            };

            var actual = await res.Object.GetLiveForexData("jsafda", "");
            Assert.AreEqual(expected.Quotes["USDGBP"], actual.Quotes["USDGBP"]);
        }

        [Test]
        [TestCase(1609229645, "2020/12/29 08:14:05")]
        [TestCase(0, "1970/01/01 01:00:00")]
        [TestCase(-1, "1969/12/31 23:59:59")]
        public void Test_ForexLiveData_UnixTimeStampConvertor(long? epoch, DateTime expected)
        {
            var forexData = new ForexLiveData()
            {
                TimeStamp = epoch.Value
            };

            forexData.CurrentDateTime.Should().BeSameDateAs(expected);
        }
    }
}
