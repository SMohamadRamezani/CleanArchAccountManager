using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using App.Utils;
using Domain.Models.User;

namespace ApiTest.Authentication
{
    public class JwtTokenGeneratorTests
    {
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly IConfiguration _configuration;

        public JwtTokenGeneratorTests()
        {
            var configurationData = new Dictionary<string, string>
        {
            { "Jwt:Key", "VerySecretKeyVerySecretKeyVerySecretKey" },
            { "Jwt:Issuer", "TestIssuer" },
            { "Jwt:Audience", "TestAudience" }
        };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configurationData)
                .Build();

            var userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            _jwtTokenGenerator = new JwtTokenGenerator(_configuration, _userManagerMock.Object);
        }

        [Fact]
        public void GenerateToken_ShouldReturn_ValidJwtToken()
        {
            // Arrange
            var user = new User { Id = "123", Email = "test@example.com" };
            _userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Admin" });

            // Act
            var token = _jwtTokenGenerator.GenerateToken(user);

            // Assert
            token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GenerateRefreshToken_ShouldReturn_NonEmptyString()
        {
            // Act
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            // Assert
            refreshToken.Should().NotBeNullOrEmpty();
            refreshToken.Length.Should().BeGreaterThan(10);
        }
    }
}
