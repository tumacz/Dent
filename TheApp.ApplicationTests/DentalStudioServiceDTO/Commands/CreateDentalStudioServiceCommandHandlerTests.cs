using Xunit;
using TheApp.Application.DentalStudioServiceDTO.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApp.Application.ApplicationUser;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using TheApp.Domain.Entities;
using TheApp.Domain.Interfaces;
using MediatR;

namespace TheApp.Application.DentalStudioServiceDTO.Commands.Tests
{
    public class CreateDentalStudioServiceCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_CreatesCarWorkshopService_WhenUserIsAuthorized()
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
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var DentaStudioRepositoryMock = new Mock<IDentalStudioRepository>();
            DentaStudioRepositoryMock.Setup(c => c.GetByEncodedName(command.DentalStudioEncodedName))
                .ReturnsAsync(dentalStudio);

            var dentalStudioServiceRepositoryMock = new Mock<IDentalStudioServiceRepository>();
            var handler = new CreateDentalStudioServiceCommandHandler(userContextMock.Object, DentaStudioRepositoryMock.Object,
                dentalStudioServiceRepositoryMock.Object);

            // act
            await handler.Handle(command, CancellationToken.None);
            // assert
            dentalStudioServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.DentalStudioService>()), Times.Once);
        }

        [Fact()]
        public async Task Handle_CreatesCarWorkshopService_WhenUserIsModerator()
        {
            // arrange
            var dentalStudio = new Domain.Entities.DentalStudio()
            {
                Id = 2,
                CreatedById = "2"
            };
            var command = new CreateDentalStudioServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                DentalStudioEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "Moderator" }));

            var DentaStudioRepositoryMock = new Mock<IDentalStudioRepository>();
            DentaStudioRepositoryMock.Setup(c => c.GetByEncodedName(command.DentalStudioEncodedName))
                .ReturnsAsync(dentalStudio);

            var dentalStudioServiceRepositoryMock = new Mock<IDentalStudioServiceRepository>();
            var handler = new CreateDentalStudioServiceCommandHandler(userContextMock.Object, DentaStudioRepositoryMock.Object,
                dentalStudioServiceRepositoryMock.Object);

            // act
            await handler.Handle(command, CancellationToken.None);
            // assert
            dentalStudioServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.DentalStudioService>()), Times.Once);
        }

        [Fact()]
        public async Task Handle_DoNotCreatesCarWorkshopService_WhenUserIsNotAuthorized()
        {
            // arrange
            var dentalStudio = new Domain.Entities.DentalStudio()
            {
                Id = 2,
                CreatedById = "2"
            };
            var command = new CreateDentalStudioServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                DentalStudioEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "Admin" }));

            var DentaStudioRepositoryMock = new Mock<IDentalStudioRepository>();
            DentaStudioRepositoryMock.Setup(c => c.GetByEncodedName(command.DentalStudioEncodedName))
                .ReturnsAsync(dentalStudio);

            var dentalStudioServiceRepositoryMock = new Mock<IDentalStudioServiceRepository>();
            var handler = new CreateDentalStudioServiceCommandHandler(userContextMock.Object, DentaStudioRepositoryMock.Object,
                dentalStudioServiceRepositoryMock.Object);

            // act
            await handler.Handle(command, CancellationToken.None);
            // assert
            dentalStudioServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.DentalStudioService>()), Times.Never);
        }

        [Fact()]
        public async Task Handle_DoNotCreatesCarWorkshopService_WhenUserIsNotAuthenticated()
        {
            // arrange
            var dentalStudio = new Domain.Entities.DentalStudio()
            {
                Id = 2,
                CreatedById = "2"
            };
            var command = new CreateDentalStudioServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                DentalStudioEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns((CurrentUser?)null);

            var DentaStudioRepositoryMock = new Mock<IDentalStudioRepository>();
            DentaStudioRepositoryMock.Setup(c => c.GetByEncodedName(command.DentalStudioEncodedName))
                .ReturnsAsync(dentalStudio);

            var dentalStudioServiceRepositoryMock = new Mock<IDentalStudioServiceRepository>();
            var handler = new CreateDentalStudioServiceCommandHandler(userContextMock.Object, DentaStudioRepositoryMock.Object,
                dentalStudioServiceRepositoryMock.Object);

            // act
            await handler.Handle(command, CancellationToken.None);
            // assert
            dentalStudioServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.DentalStudioService>()), Times.Never);
        }
    }
}