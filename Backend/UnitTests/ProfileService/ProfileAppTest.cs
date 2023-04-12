using ProfileService.Application;
using ProfileService.Data.Models;
using ProfileService.Data;
using Moq;

namespace profileService.Tests
{
    [TestFixture]
    public class ProfileAppTest
    {
        private Mock<IProfileRepository> profileRepositoryMock;
        private ProfileApp profileApp;

        [SetUp]
        public void Setup()
        {
            profileRepositoryMock = new Mock<IProfileRepository>();
            profileApp = new ProfileApp(profileRepositoryMock.Object);
        }

        [Test]
        public async Task FollowUser_ShouldCallRepository()
        {
            Guid userId = Guid.NewGuid();
            Guid userToFollow = Guid.NewGuid();

            await profileApp.FollowUser(userId, userToFollow);

            profileRepositoryMock.Verify(x => x.FollowUser(userId, userToFollow), Times.Once);
        }

        [Test]
        public void GetProfile_ShouldCallRepository()
        {
            Guid userId = Guid.NewGuid();

            profileApp.GetProfile(userId);

            profileRepositoryMock.Verify(x => x.GetProfile(userId), Times.Once);
        }

        [Test]
        public async Task UnfollowUser_ShouldCallRepository()
        {
            Guid userId = Guid.NewGuid();
            Guid userToUnfollow = Guid.NewGuid();

            await profileApp.UnfollowUser(userId, userToUnfollow);

            profileRepositoryMock.Verify(x => x.UnfollowUser(userId, userToUnfollow), Times.Once);
        }

        [Test]
        public async Task UpdateProfile_ShouldCallRepository()
        {
            Guid userId = Guid.NewGuid();
            Profile profileData = new Profile { Username = "John Doe", Biography = "hi" };

            await profileApp.UpdateProfile(userId, profileData);

            profileRepositoryMock.Verify(x => x.UpdateProfile(userId, profileData), Times.Once);
        }

        [Test]
        public void GetAll_ShouldCallRepository()
        {
            List<Profile> expectedProfiles = new List<Profile>
            {
                new Profile { OwnerId = Guid.NewGuid(), Username = "John Doe", Biography = "hi" },
                new Profile { OwnerId = Guid.NewGuid(), Username = "Jane Doe", Biography = "bye" }
            };

            profileRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(expectedProfiles);

            Task<List<Profile>> actualProfilesTask = profileApp.GetAll();

            profileRepositoryMock.Verify(x => x.GetAll(), Times.Once);

            List<Profile> actualProfiles = actualProfilesTask.Result;
            Assert.That(actualProfiles, Has.Count.EqualTo(expectedProfiles.Count));
            Assert.Multiple(() =>
            {
                Assert.That(actualProfiles[0].OwnerId, Is.EqualTo(expectedProfiles[0].OwnerId));
                Assert.That(actualProfiles[0].Username, Is.EqualTo(expectedProfiles[0].Username));
                Assert.That(actualProfiles[0].Biography, Is.EqualTo(expectedProfiles[0].Biography));
                Assert.That(actualProfiles[1].OwnerId, Is.EqualTo(expectedProfiles[1].OwnerId));
                Assert.That(actualProfiles[1].Username, Is.EqualTo(expectedProfiles[1].Username));
                Assert.That(actualProfiles[1].Biography, Is.EqualTo(expectedProfiles[1].Biography));
            });
        }
    }
}
