using NUnit.Framework;
using ProfileService.Api;
using ProfileService.Data.Models;

namespace profileService.Tests
{
    [TestFixture]
    public class ProfileConversionTest
    {
        [Test]
        public void AsDto_ReturnsCorrectProfileObject()
        {
            // Arrange
            var input = new ProfileService.Data.Models.ProfileData
            {
                Username = "testuser",
                // Add any other properties you want to test here
            };

            bool isBlocked = true;

            // Act
            var result = input.AsDto(isBlocked);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Blocked, Is.EqualTo(isBlocked));
                Assert.That(result.DisplayName, Is.EqualTo(input.Username));
            });
        }
    }
}
