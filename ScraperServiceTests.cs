using Moq;
using Moq.Protected;
using Scraper.Core.Interfaces;
using Scraper.Core.Services;
using System.Net;
using System.Net.Http;

namespace Scraper.Core.Tests
{
    public class ScraperServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly ScraperService _scraperService;

        public ScraperServiceTests()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _scraperService = new ScraperService(_httpClientFactoryMock.Object);
        }

        [Fact]
        public async Task GetSearchCountAsync_ReturnsMinusOne_WhenNoUrlsMatch()
        {
            // Arrange
            var keyWord = "test";
            var searchUrl = "test.com";
            var maxCount = 10;
            var expectedPositions = new List<int> { -1 };

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("<html><body><a href=\"test.com\">Link</a></body></html>")
            };

            httpMessageHandlerMock
             .Protected()
             .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
             .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(httpMessageHandlerMock.Object);

            _httpClientFactoryMock
                .Setup(f => f.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            // Act
            var result = await _scraperService.GetSearchCountAsync(keyWord, searchUrl, maxCount);

            // Assert
            Assert.Equal(expectedPositions, result);
        }
    }
}