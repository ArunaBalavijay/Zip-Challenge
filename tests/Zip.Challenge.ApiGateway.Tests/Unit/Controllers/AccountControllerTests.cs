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
    public class AccountControllerTests
    {
        [Fact]
        public async Task account_controller_get_should_call_service_list()
        {
            var accountServiceMock = new Mock<IAccountService>();
            var userServiceMock = new Mock<IUserService>();
            accountServiceMock.Setup(svc => svc.ListAsync()).ReturnsAsync(new List<Account>());
            var controller = new AccountController(accountServiceMock.Object, userServiceMock.Object);

            var result = await controller.Get();
            result.Should().NotBeNull();

            accountServiceMock.Verify(svc => svc.ListAsync(), Times.Once);
        }

        [Fact]
        public async Task account_controller_post_should_call_service_Create()
        {
            var testData = GetTestData();
            var accountServiceMock = new Mock<IAccountService>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(svc => svc.GetAsync(testData.UserEmail)).ReturnsAsync(new User { Email = "test@test.com", Salary = 1500, Expense = 500 });
            var controller = new AccountController(accountServiceMock.Object, userServiceMock.Object);

            var result = await controller.Post(testData);

            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();

            userServiceMock.Verify(svc => svc.GetAsync(testData.UserEmail), Times.Once);
            accountServiceMock.Verify(svc => svc.CreateAsync(testData), Times.Once);
        }

        [Fact]
        public async Task account_controller_post_should_return_user_error()
        {
            var testData = GetTestData();
            var accountServiceMock = new Mock<IAccountService>();
            var userServiceMock = new Mock<IUserService>();
            var controller = new AccountController(accountServiceMock.Object, userServiceMock.Object);

            var result = await controller.Post(testData);

            var contentResult = result as BadRequestObjectResult;
            contentResult.Should().NotBeNull();

            userServiceMock.Verify(svc => svc.GetAsync(testData.UserEmail), Times.Once);
            accountServiceMock.Verify(svc => svc.CreateAsync(testData), Times.Never);
        }

        [Fact]
        public async Task account_controller_post_should_return_account_error()
        {
            var testData = GetTestData();
            var accountServiceMock = new Mock<IAccountService>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(svc => svc.GetAsync(testData.UserEmail)).ReturnsAsync(new User { Email = "test@test.com", Salary = 1000, Expense = 500 });
            var controller = new AccountController(accountServiceMock.Object, userServiceMock.Object);

            var result = await controller.Post(testData);

            var contentResult = result as UnprocessableEntityObjectResult;
            contentResult.Should().NotBeNull();

            userServiceMock.Verify(svc => svc.GetAsync(testData.UserEmail), Times.Once);
            accountServiceMock.Verify(svc => svc.CreateAsync(testData), Times.Never);
        }

        private CreateAccount GetTestData()
        {
            return new CreateAccount
            {
                UserEmail = "test@test.com"
            };
        }
    }
}
