using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheApp.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Net;

namespace TheApp.MVC.Controllers.Tests
{
    public class HomeControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> _factory;

        public HomeControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact()]
        public async Task About_ReturnsVievWithRenderModel()
        {
            //arange
            var client = _factory.CreateClient();

            //act
            var response = await client.GetAsync("/Home/About");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("Info Model Title")
                   .And.Contain("Info Model Description")
                   .And.Contain("#dental")
                   .And.Contain("#studio")
                   .And.Contain("app")
                   .And.Contain("#First Tag Test")
                   .And.Contain("#Second Tag Test");
        }
    }
}