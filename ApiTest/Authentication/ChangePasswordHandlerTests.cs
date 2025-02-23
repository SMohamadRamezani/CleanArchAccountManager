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
    public class ChangePasswordHandlerTests
    {
        private readonly Mock<IUserRepository> _userManagerMock;
        private readonly Mock<IMessageService> _messageService;
        private readonly ChangePasswordHandler _handler;
        public ChangePasswordHandlerTests()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<IUserRepository>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            _messageService = new Mock<IMessageService>();
            _handler = new ChangePasswordHandler(_userManagerMock.Object,_messageService.Object);
        }
        [Fact]
        public async Task Handle_ValidRequest_ShouldChangePasswordSuccessfully()
        {
            // Arrange
            var user = new User
            {
                Id = "123",
                UserName = "testuser",
                Email = "test@example.com"
            };

            var command = new ChangePasswordRequest
            {
                UserName = user.UserName,
                OldPassword = "OldPassword123!",
                NewPassword = "NewPassword123!"
            };

            _userManagerMock.Setup(um => um.GetUserByIdAsync(user.Id))
                .ReturnsAsync(user);

            _userManagerMock.Setup(um => um.ChangePasswordAsync(user.UserName, command.OldPassword, command.NewPassword));

            var handler = new ChangePasswordHandler(_userManagerMock.Object, _messageService.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(true);

            // بررسی اینکه `ChangePasswordAsync` دقیقا یک‌بار فراخوانی شده است
            _userManagerMock.Verify(um => um.ChangePasswordAsync(user.UserName, command.OldPassword, command.NewPassword), Times.Once);
        }
    }
}
