using Xunit;
using CoachBuddy.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using CoachBuddy.Application.Client;
using MediatR;
using Moq;
using CoachBuddy.Application.Client.Queries.GetAllClients;
using Microsoft.AspNetCore.TestHost;
using FluentAssertions;
using System.Net;

namespace CoachBuddy.MVC.Controllers.Tests
{
    public class ClientControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ClientControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Fact()]
        public async Task Index_ReturnsViewWithExpectedData_ForExistingClients()
        {
            // arrange

            var clients = new List<ClientDto>()
            {
                new ClientDto()
                {
                    Name="Client 1",
                },

                new ClientDto()
                {
                    Name="Client 2",
                },

                new ClientDto()
                {
                    Name="Client 3",
                },
            };

            var mediatorMock = new Mock<IMediator>();

          //  mediatorMock.Setup(m => m.Send(It.IsAny<GetAllClientsQuery>(), It.IsAny<CancellationToken>()))
          //      .ReturnsAsync(clients);

            var client = _factory
                .WithWebHostBuilder(builder => builder.ConfigureTestServices(trainings => trainings.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            // act

            var response = await client.GetAsync("/Client/Index");

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<h1>Clients</h1>")
                .And.Contain("Client 1")
                .And.Contain("Client 2")
                .And.Contain("Client 3");
        }

        [Fact()]
        public async Task Index_ReturnsEmptyView_WhenNoClientsExist()
        {
            // arrange

            var clients = new List<ClientDto>();

            var mediatorMock = new Mock<IMediator>();

        //    mediatorMock.Setup(m => m.Send(It.IsAny<GetAllClientsQuery>(), It.IsAny<CancellationToken>()))
           //     .ReturnsAsync(clients);

            var client = _factory
                .WithWebHostBuilder(builder => builder.ConfigureTestServices(trainings => trainings.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            // act

            var response = await client.GetAsync("/Client/Index");

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().NotContain("div class=\"card m-3\"");
        }
    }
}