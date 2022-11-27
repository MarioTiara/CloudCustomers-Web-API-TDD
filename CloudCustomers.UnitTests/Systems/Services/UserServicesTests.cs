using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Utils;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class UserServicesTests
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrange
            var expectedResponse= UserFixture.GetTestUsers();
            var hanlderMock= MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            var httpClient= new HttpClient(hanlderMock.Object);
            var sut= new UsersService(httpClient);

            //Act
             await sut.GetAllUsers();
            //Assert
            hanlderMock.
                Protected().
                Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
        }
    }
}
