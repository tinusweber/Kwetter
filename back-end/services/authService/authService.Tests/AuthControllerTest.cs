using authService.Api.Constants;
using authService.Api.Controllers;
using authService.Api.Models;
using authService.DomainModels;
using authService.DomainModels.Requests;
using MassTransit;
using MessagingModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace authService.Tests
{
    [TestFixture]
    public class AuthControllerTest
    {
        private AuthController _authController;
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private Mock<IPublishEndpoint> _mockPublishEndpoint;

        [SetUp]
        public void Setup()
        {
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);
            _mockPublishEndpoint = new Mock<IPublishEndpoint>();

            _authController = new AuthController(_mockUserManager.Object, _mockRoleManager.Object, _mockPublishEndpoint.Object);
        }

        [Test]
        public async Task UpdatePassword_ReturnsBadRequest_WhenPasswordsDoNotMatch()
        {
            // Arrange
            var request = new UpdatePasswordRequest { Current = "oldPassword", New = "newPassword", NewConfirm = "differentPassword" };

            // Act
            var result = await _authController.UpdatePassword(request);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdatePassword_ReturnsOk_WhenPasswordIsUpdated()
        {
            // Arrange
            var request = new UpdatePasswordRequest { Current = "oldPassword", New = "newPassword", NewConfirm = "newPassword" };
            var user = new ApplicationUser { UserName = "testuser" };
            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.ChangePasswordAsync(user, request.Current, request.New)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authController.UpdatePassword(request);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

/*
        [Test]
        public async Task Register_ReturnsOk_WhenUserIsCreated()
        {
            // Arrange
            var request = new SingupRequest();
            var user = new ApplicationUser();
            var identityResult = IdentityResult.Success;

            _mockUserManager.Setup(x => x.CreateAsync(user, request.Password)).ReturnsAsync(identityResult);
            _mockUserManager.Setup(x => x.AddToRoleAsync(user, Roles.User)).ReturnsAsync(IdentityResult.Success);
            _mockPublishEndpoint.Setup(x => x.Publish<IUserRegisteredEvent>(It.IsAny<object>(), default)).Returns(Task.CompletedTask);

            _authController.ModelState.Clear(); // Clear model state to ensure there are no validation errors.

            // Act
            var result = await _authController.Register(request);

            // Assert
            Assert.That(result, Is.InstanceOf<OkResult>());
        }
*/
        [Test]
        public async Task Register_ReturnsBadRequest_WhenUserIsNotCreated()
        {
            // Arrange
            var request = new SingupRequest();
            var identityResult = IdentityResult.Failed(new IdentityError { Description = "Error description" });
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(identityResult);

            _authController.ModelState.Clear(); // Clear model state to ensure there are no validation errors.

            // Act
            var result = await _authController.Register(request);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
    }
}
