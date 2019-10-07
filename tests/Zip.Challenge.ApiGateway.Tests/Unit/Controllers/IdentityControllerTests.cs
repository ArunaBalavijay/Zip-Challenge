using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Zip.Challenge.ApiGateway.Controllers;
using Zip.Challenge.ApiGateway.Services;
using Zip.Challenge.Common.Commands;
using Zip.Challenge.Common.Dto;

namespace Zip.Challenge.ApiGateway.Tests.Unit.Controllers
{
    public class IdentityControllerTests
    {
        [Fact]
        public async Task identity_controller_get_should_call_service_list()
        {
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(svc => svc.ListAsync()).ReturnsAsync(new List<User>());
            var controller = new IdentityController(userServiceMock.Object);

            var result = await controller.Get();
            result.Should().NotBeNull();

            userServiceMock.Verify(svc => svc.ListAsync(), Times.Once);
        }

        [Fact]
        public async Task identity_controller_get_should_call_service_get()
        {
            var testEmail = "test@test.com";
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(svc => svc.GetAsync(testEmail)).ReturnsAsync(new User{ Email = testEmail });
            var controller = new IdentityController(userServiceMock.Object);

            var result = await controller.Get(testEmail);
            result.Email.Should().BeSameAs(testEmail);

            userServiceMock.Verify(svc => svc.GetAsync(testEmail), Times.Once);
        }

        [Fact]
        public async Task identity_controller_post_should_call_service_Register()
        {
            var testData = GetTestData();
            var userServiceMock = new Mock<IUserService>();
            var controller = new IdentityController(userServiceMock.Object);

            var result = await controller.Post(testData);

            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();

            userServiceMock.Verify(svc => svc.GetAsync(testData.Email), Times.Once);
            userServiceMock.Verify(svc => svc.RegisterAsync(testData), Times.Once);
        }

        [Fact]
        public async Task identity_controller_post_should_return_error()
        {
            var testData = GetTestData();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(svc => svc.GetAsync(testData.Email)).ReturnsAsync(new User { Email = testData.Email });
            var controller = new IdentityController(userServiceMock.Object);

            var result = await controller.Post(testData);

            var contentResult = result as UnprocessableEntityObjectResult;
            contentResult.Should().NotBeNull();

            userServiceMock.Verify(svc => svc.GetAsync(testData.Email), Times.Once);
            userServiceMock.Verify(svc => svc.RegisterAsync(testData), Times.Never);
        }

        private CreateUser GetTestData()
        {
            return new CreateUser
            {
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "Test"
            };
        }
    }
}
