using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Utils
{
    public class UserControllerTest
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            //arrange
            var mockUUserService = new Mock<IUsersService>();
            mockUUserService.Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());
            var sut = new UsersController(mockUUserService.Object);
            //act
            var result= (OkObjectResult)await sut.Get();
            //Assert
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Get_OnSuccess_InvokeUserService()
        {
            var mockUUserService = new Mock<IUsersService>();
            mockUUserService.Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());
            var sut = new UsersController(mockUUserService.Object);
            var result = await sut.Get();
            mockUUserService.Verify(
                service=>service.GetAllUsers(), 
                Times.Once());
        }
    }
}
