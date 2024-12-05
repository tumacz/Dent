using Xunit;
using TheApp.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using TheApp.Application.DataTransferObjects.Queries.GetAllDentaStudiosQuery;
using TheApp.Application.DataTransferObjects;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using System.Net;

namespace TheApp.MVC.Controllers.Tests
{
    public class DentalStudioControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public DentalStudioControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact()]
        public async Task Index_ReturnsViewWithExpectedData_ForExistingDentalStudios()
        {
            //arrange
            var dentalStudios = new List<DentalStudioDTO>()
            {
                new()
                {
                    Name = "Test",
                },
                new()
                {
                    Name = "Dental Studio",
                },
                new()
                {
                    Name = "Name"
                }
            };

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(c => c.Send(It.IsAny<GetAllDentalStudiosQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dentalStudios);

            //cnf mediatorMock
            var client = _factory
                .WithWebHostBuilder(builder => builder.ConfigureTestServices(services => services.AddScoped(mM => mediatorMock.Object)))
                .CreateClient();

            //act
            var response = await client.GetAsync("/DentalStudio/Index");

            //assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("Test")
                   .And.Contain("Dental Studio")
                   .And.Contain("Name");
        }

        [Fact()]
        public async Task Index_ReturnsEmptyView_WhenNoDentalStudioExists()
        {
            //arrange
            var dentalStudios = new List<DentalStudioDTO>();

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(c => c.Send(It.IsAny<GetAllDentalStudiosQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dentalStudios);

            //cnf mediatorMock
            var client = _factory
                .WithWebHostBuilder(builder => builder.ConfigureTestServices(services => services.AddScoped(mM => mediatorMock.Object)))
                .CreateClient();

            //act
            var response = await client.GetAsync("/DentalStudio/Index");

            //assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().NotContain("<div class=\"card m-3\"");
        }
    }
}