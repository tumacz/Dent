using Xunit;
using TheApp.Application.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using FluentAssertions;

namespace TheApp.Application.ApplicationUser.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "test@example.com"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User")
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(x => x.User).Returns(user);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

            var userContext = new UserContext(httpContextAccessorMock.Object);

            // Act
            var result = userContext.GetCurrentUser();

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be("1");
            result.Email.Should().Be("test@example.com");
            result.Roles.Should().ContainInOrder("Admin", "User");
        }

    }
}