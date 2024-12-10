using Xunit;
using TheApp.Application.ApplicationUser;
using Moq;
using TheApp.Domain.Interfaces;

namespace TheApp.Application.DentalStudioServiceDTO.Commands.Tests
{
    public class CreateDentalStudioServiceCommandHandlerTests
    {
        [Theory]
        [InlineData("1", "test@test.com", new[] { "User" }, true)]   // Authorized user
        [InlineData("2", "test@test.com", new[] { "Moderator" }, true)]  // Authorized user (Moderator)
        [InlineData("2", "test@test.com", new[] { "Admin" }, false)]  // Unauthorized user
        [InlineData(null, null, null, false)]  // Not authenticated user
        public async Task Handle_CreatesDentalStudioService_WhenUserIsAuthorizedOrNot(
            string userId, string email, string[] roles, bool shouldCreate)
        {
            // arrange
            var dentalStudio = new Domain.Entities.DentalStudio()
            {
                Id = 1,
                CreatedById = "1"
            };
            var command = new CreateDentalStudioServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                DentalStudioEncodedName= "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(userId == null ? null : new CurrentUser(userId, email, roles));

            var DentaStudioRepositoryMock = new Mock<IDentalStudioRepository>();
            DentaStudioRepositoryMock.Setup(c => c.GetByEncodedName(command.DentalStudioEncodedName))
                .ReturnsAsync(dentalStudio);

            var dentalStudioServiceRepositoryMock = new Mock<IDentalStudioServiceRepository>();

            var handler = new CreateDentalStudioServiceCommandHandler(userContextMock.Object, DentaStudioRepositoryMock.Object,
                dentalStudioServiceRepositoryMock.Object);

            // act
            await handler.Handle(command, CancellationToken.None);
            // assert
            if (shouldCreate)
            {
                dentalStudioServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.DentalStudioService>()), Times.Once);
            }
            else
            {
                dentalStudioServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.DentalStudioService>()), Times.Never);
            }
        }
    }
}