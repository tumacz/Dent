using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using TheApp.Application.DentalStudios.Queries.GetAllDentalStudios;
using TheApp.Application.DentalStudio_CQRS.DataTransferObjects;

namespace TheApp.MVC.Controllers.Tests
{
    public class DentalStudioControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public DentalStudioControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Index_ReturnsViewWithExpectedData_ForExistingDentalStudios()
        {
            // Arrange
            var dentalStudios = new List<DentalStudioListItemDTO>()
            {
                new() { Name = "Test", EncodedName = "test", City = "City1" },
                new() { Name = "Dental Studio", EncodedName = "dental-studio", City = "City2" },
                new() { Name = "Name", EncodedName = "name", City = "City3" }
            };

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllDentalStudiosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(dentalStudios);

            var client = _factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services =>
                        services.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            // Act
            var response = await client.GetAsync("/DentalStudio/Index");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("Test")
                   .And.Contain("Dental Studio")
                   .And.Contain("Name");
        }

        [Fact]
        public async Task Index_ReturnsEmptyView_WhenNoDentalStudioExists()
        {
            // Arrange
            var dentalStudios = new List<DentalStudioListItemDTO>(); // ✅ pusty, poprawny typ DTO

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllDentalStudiosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(dentalStudios);

            var client = _factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services =>
                        services.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            // Act
            var response = await client.GetAsync("/DentalStudio/Index");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().NotContain("<div class=\"card m-3\"");
        }
    }
}
