using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.UnitTests.Utils
{
    public class UserControllerTest
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            //arrange
            var mockUserService = new Mock<IUsersService>();
            mockUserService.Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());
            var sut = new UsersController(mockUserService.Object);
            //act
            var result= (OkObjectResult)await sut.Get();
            //Assert
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Get_OnSuccess_InvokeUserService()
        {
            //arrange
            var mockUserService = new Mock<IUsersService>();
            mockUserService.Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());
            var sut = new UsersController(mockUserService.Object);
           
            //act
            var result = await sut.Get();

            //assert
            mockUserService.Verify(
                service=>service.GetAllUsers(), 
                Times.Once());

            
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnsListOfUsers()
        {
            //arrange
            var mockUsersService= new Mock<IUsersService>();
            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());
            var sut = new UsersController(mockUsersService.Object);

            //Act
            var result = await sut.Get();

            //Asert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult=(OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();

        }
    }
}
