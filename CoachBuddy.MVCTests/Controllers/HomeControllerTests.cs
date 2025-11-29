using Xunit;
using CoachBuddy.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CoachBuddy.Controllers.Tests
{
    public class HomeControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public HomeControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact()]
        public async Task About_ReturnsViewWithRenderModel()
        {
            // arrange

            var client = _factory.CreateClient();

            // act

            var response = await client.GetAsync("/Home/About");

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<h1>CoachBuddy application</h1>")
                .And.Contain("<h2>Some description</h2>")
                .And.Contain("<li>coach</li>")
                .And.Contain("<li>client</li>")
                .And.Contain("<li>program</li>");
        }
    }
}