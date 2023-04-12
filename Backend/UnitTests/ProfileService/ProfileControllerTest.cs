using Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using ProfileService.Api;
using ProfileService.Api.Controllers;
using ProfileService.Application;
using ProfileService.Data;
using ProfileService.Data.Models;
using System.Security.Claims;

namespace profileService.Tests
{
    [TestFixture]
    public class ProfileControllerTest
    {
        private IProfileRepository mockRepository;
        private ProfileApp mockProfileApp;
        private ProfileController controller;

        [SetUp]
        public void Setup()
        {
            mockRepository = Substitute.For<IProfileRepository>();
            mockProfileApp = new ProfileApp(mockRepository);
            controller = new ProfileController(mockProfileApp);
        }
/*
        [Test]
        public async Task GetAll_ReturnsListOfProfiles()
        {
            // Arrange
            var expectedProfiles = new List<ProfileData>()
            {
                new ProfileData() { OwnerId = Guid.NewGuid(), Username = "johndoe", Biography = "Hello world", ProfilePictureBase64 = null },
                new ProfileData() { OwnerId = Guid.NewGuid(), Username = "janedoe", Biography = "Lorem ipsum", ProfilePictureBase64 = null },
            };

            mockProfileApp.GetAll().Returns(Task.FromResult(expectedProfiles));
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Items["userId"] = Guid.NewGuid();

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<List<Profile>>());
            Assert.That(result, Has.Count.EqualTo(expectedProfiles.Count));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].OwnerId, Is.EqualTo(expectedProfiles[0].OwnerId));
                Assert.That(result[0].DisplayName, Is.EqualTo(expectedProfiles[0].Username));
                Assert.That(result[0].Biography, Is.EqualTo(expectedProfiles[0].Biography));
                Assert.That(result[0].ProfilePictureBase64, Is.EqualTo(expectedProfiles[0].ProfilePictureBase64));
                Assert.That(result[1].OwnerId, Is.EqualTo(expectedProfiles[1].OwnerId));
                Assert.That(result[1].DisplayName, Is.EqualTo(expectedProfiles[1].Username));
                Assert.That(result[1].Biography, Is.EqualTo(expectedProfiles[1].Biography));
                Assert.That(result[1].ProfilePictureBase64, Is.EqualTo(expectedProfiles[1].ProfilePictureBase64));
            });
        }

        [Test]
        public void Get_ReturnsUserProfile()
        {
            var expectedProfile = new ProfileData()
            {
                OwnerId = Guid.NewGuid(),
                Username = "johndoe",
                Biography = "I am John Doe",
                ProfilePictureBase64 = "base64"
            };
            var profileController = new ProfileController(mockProfileApp);

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = Mock.Of<ClaimsPrincipal>() }
            };
            controllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer {token}";
            profileController.ControllerContext = controllerContext;

            mockProfileApp.GetProfile(Arg.Any<Guid>()).Returns(expectedProfile);

            // Act
            var result = controller.Get();

            // Assert
            Assert.That(result, Is.InstanceOf<Profile>());
            Assert.Multiple(() =>
            {
                Assert.That(result.OwnerId, Is.EqualTo(expectedProfile.OwnerId));
                Assert.That(result.DisplayName, Is.EqualTo(expectedProfile.Username));
                Assert.That(result.Biography, Is.EqualTo(expectedProfile.Biography));
                Assert.That(result.ProfilePictureBase64, Is.EqualTo(expectedProfile.ProfilePictureBase64));
            });
        }

        [Test]
        public void GetById_ReturnsProfileWithBlockStatus()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var expectedProfile = new ProfileData() { OwnerId = profileId, Username = "John Doe", Biography = "Hello world", ProfilePictureBase64 = null };

            mockProfileApp.GetProfile(profileId).Returns(expectedProfile);
            mockProfileApp.GetProfile(Arg.Any<Guid>()).Returns(new ProfileData() { BlockedUsers = new List<Guid>() { profileId } });
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Items["userId"] = Guid.NewGuid();

            // Act
            var result = controller.GetById(profileId.ToString());

            // Assert
            Assert.That(result, Is.InstanceOf<Profile>());
            Assert.Multiple(() =>
            {
                Assert.That(result.OwnerId, Is.EqualTo(expectedProfile.OwnerId));
                Assert.That(result.DisplayName, Is.EqualTo(expectedProfile.Username));
                Assert.That(result.Biography, Is.EqualTo(expectedProfile.Biography));
                Assert.That(result.ProfilePictureBase64, Is.EqualTo(expectedProfile.ProfilePictureBase64));
                Assert.That(result.Blocked, Is.True);
            });
        }
*/
        [Test]
        public async Task UpdateProfile_ReturnsOk()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileUpdate = new ProfileUpdate() { DisplayName = "John Doe", Biography = "Hello world", ProfilePictureBase64 = null };
            mockProfileApp.UpdateProfile(profileId, Arg.Any<ProfileService.Data.Models.Profile>()).Returns(Task.FromResult<ProfileService.Data.Models.Profile>(new ProfileService.Data.Models.Profile() { OwnerId = profileId }));
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await controller.UpdateProfile(profileId.ToString(), profileUpdate);

            // Assert
            Assert.That(result, Is.InstanceOf<OkResult>());
        }
    }
}
