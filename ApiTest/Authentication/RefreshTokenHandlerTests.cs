using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using App.Authentication.Request;
using App.Authentication.Handlers;
using App.Interfaces;
using Domain.Interfaces;
using Domain.Models.User;

namespace ApiTest.Authentication
{
    public class RefreshTokenHandlerTests
    {
        private readonly Mock<IUserRepository> _userManagerMock;
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private readonly Mock<IMessageService> _messageService;
        private readonly RefreshTokenHandler _handler;

        public RefreshTokenHandlerTests()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<IUserRepository>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _messageService = new Mock<IMessageService>();
            _handler = new RefreshTokenHandler(_userManagerMock.Object, _jwtTokenGeneratorMock.Object, _messageService.Object);
        }

        [Fact]
        public async Task Handle_ValidRefreshToken_ShouldReturn_NewJwtToken()
        {
            // Arrange
            var user = new User
            {
                Id = "123",
                RefreshToken = "valid_refresh_token",
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
            };
            _userManagerMock.Setup(um => um.GetAllUsersAsync());
            _jwtTokenGeneratorMock.Setup(j => j.GenerateToken(user)).Returns("new_jwt_token");
            _jwtTokenGeneratorMock.Setup(j => j.GenerateRefreshToken()).Returns("new_refresh_token");

            var command = new RefreshTokenRequest { RefreshToken = "valid_refresh_token" };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be("new_jwt_token");
            user.RefreshToken.Should().Be("new_refresh_token");
            _userManagerMock.Verify(um => um.UpdateUserAsync(user), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRefreshToken_ShouldThrowException()
        {
            // Arrange
            var command = new RefreshTokenRequest { RefreshToken = "invalid_token" };

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("توکن نامعتبر است");
        }
    }
}
