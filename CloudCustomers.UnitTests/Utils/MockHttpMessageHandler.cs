using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Utils
{
    internal class MockHttpMessageHandler <T>
    {
        internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse)
        {
            var mocrespons = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };

            mocrespons.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var handlerMock= new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mocrespons);

            return handlerMock;
        }
    }
}
