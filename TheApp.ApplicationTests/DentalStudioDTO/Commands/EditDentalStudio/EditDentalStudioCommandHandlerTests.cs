using Xunit;
using Moq;
using TheApp.Domain.Interfaces;
using TheApp.Application.ApplicationUser;

namespace TheApp.Application.DataTransferObjects.Commands.EditDentalStudio.Tests
{
    public class EditDentalStudioCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_WithValidData_InduceCommit()
        {
            // Arrange
            var dentalStudio = new Domain.Entities.DentalStudio()
            {
                CreatedById = "1",
                Name = "Test Name",
                Description = "Test Description",
                ContactDetails = new()
                {
                    Street = "street",
                    City = "City",
                    PhoneNumber = "12345",
                    PostalCode = "12345",
                    Link = "Link"
                }
            };
            dentalStudio.EncodeName();

            var currentUser = new CurrentUser("1", "test@email.com", new[] { "Moderator" });

            var command = new EditDentalStudioCommand()
            {
                Description = "New Description Test",
                EncodedName = "new-test-name",
                IsEditable =true,
                PhoneNumber= "12345",
                PostalCode = "12345",
                Link = "Link",
                City ="City",
                Street = "Street"
            };

            var repositoryMock = new Mock<IDentalStudioRepository>();
            repositoryMock.Setup(r => r.GetByEncodedName(command.EncodedName))
                          .ReturnsAsync(dentalStudio);
            repositoryMock.Setup(r => r.Commit()).Returns(Task.CompletedTask);

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                           .Returns(currentUser);

            var handler = new EditDentalStudioCommandHandler(repositoryMock.Object, userContextMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            repositoryMock.Verify(r => r.GetByEncodedName(command.EncodedName!), Times.Once);
            repositoryMock.Verify(r => r.Commit(), Times.Once);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("New Description Test", dentalStudio.Description);
        }
    }
}