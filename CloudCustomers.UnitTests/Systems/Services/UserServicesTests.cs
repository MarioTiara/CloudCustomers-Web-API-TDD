using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Utils;
using FluentAssertions;
using Microsoft.Extensions.Options;
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
            var endpoint = "https://exampl.com/users";
            var hanlderMock= MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            var httpClient= new HttpClient(hanlderMock.Object);
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut= new UsersService(httpClient, config);

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

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
        {
            //Arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var endpoint = "https://exampl.com/users";
            var hanlderMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(hanlderMock.Object);
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();
            //Assert
            result.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task GetAllUsers_WhenHit404_ReturnsEmptyListOfUsers()
        {
            //Arrange
            var endpoint = "https://exampl.com/users";
            var hanlderMock = MockHttpMessageHandler<User>
                .SetupReturn404();
            var httpClient = new HttpClient(hanlderMock.Object);
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();
            //Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //Arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var endpoint = "https://exampl.com/users";
            var hanlderMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(hanlderMock.Object);
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient,config);

            //Act
            var result = await sut.GetAllUsers();
            //Assert
            result.Count.Should().Be(3);
        }

        [Fact]
        public async Task GetAllSuers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            var expectedResponse = UserFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var hanlderMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse, endpoint);
            var httpClient = new HttpClient(hanlderMock.Object);

            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();
            //Assert
            hanlderMock.
                Protected().
                Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get
                && req.RequestUri.ToString()==endpoint),
                ItExpr.IsAny<CancellationToken>()
                );
        }
    }
    
}
