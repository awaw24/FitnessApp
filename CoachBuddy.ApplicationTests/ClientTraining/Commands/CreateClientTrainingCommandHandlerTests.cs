using Xunit;
using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Application.CoachBuddy;
using MediatR;
using Moq;
using FluentAssertions;
using CoachBuddy.Domain.Interfaces.Client;

namespace CoachBuddy.Application.ClientTraining.Commands.Tests
{
    public class CreateCarWorkshopServiceCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_CreatesClientTrainings_WhenUserIsAuthorized()
        {
            // arrange

            var client = new Domain.Entities.Client.Client()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateClientTrainingCommand()
            {
                Date = new DateTime(2024, 9, 14),
                Description = "Service description",
                ClientEncodedName = "client1"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));


            var clientRepositoryMock = new Mock<IClientRepository>();
            clientRepositoryMock.Setup(c => c.GetByEncodedName(command.ClientEncodedName))
                .ReturnsAsync(client);

            var clientTrainingRepositoryMock = new Mock<IClientTrainingRepository>();

            var handler = new CreateClientTrainingCommandHandler(userContextMock.Object, clientRepositoryMock.Object,
                clientTrainingRepositoryMock.Object);

            // act

            var result = await handler.Handle(command, CancellationToken.None);

            // assert

            result.Should().Be(Unit.Value);
            clientTrainingRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.Client.ClientTraining>()), Times.Once);

        }


        [Fact()]
        public async Task Handle_CreatesCarWorkshopService_WhenUserIsModerator()
        {
            // arrange

            var client = new Domain.Entities.Client.Client()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateClientTrainingCommand()
            {
                Date = new DateTime(2024, 9, 14),
                Description = "Service description",
                ClientEncodedName = "client1"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("2", "test@test.com", new[] { "Moderator" }));


            var clientRepositoryMock = new Mock<IClientRepository>();
            clientRepositoryMock.Setup(c => c.GetByEncodedName(command.ClientEncodedName))
                .ReturnsAsync(client);

            var clientTrainingRepositoryMock = new Mock<IClientTrainingRepository>();

            var handler = new CreateClientTrainingCommandHandler(userContextMock.Object, clientRepositoryMock.Object,
                clientTrainingRepositoryMock.Object);

            // act

            var result = await handler.Handle(command, CancellationToken.None);

            // assert

            result.Should().Be(Unit.Value);
            clientTrainingRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.Client.ClientTraining>()), Times.Once);

        }


        [Fact()]
        public async Task Handle_DoesntCreateCarWorkshopService_WhenUserIsNotAuthorized()
        {
            // arrange

            var client = new Domain.Entities.Client.Client()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateClientTrainingCommand()
            {
                Date = new DateTime(2024, 9, 14),
                Description = "Service description",
                ClientEncodedName = "client1"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("2", "test@test.com", new[] { "User" }));


            var clientRepositoryMock = new Mock<IClientRepository>();
            clientRepositoryMock.Setup(c => c.GetByEncodedName(command.ClientEncodedName))
                .ReturnsAsync(client);

            var clientTrainingRepositoryMock = new Mock<IClientTrainingRepository>();

            var handler = new CreateClientTrainingCommandHandler(userContextMock.Object, clientRepositoryMock.Object,
                clientTrainingRepositoryMock.Object);

            // act

            var result = await handler.Handle(command, CancellationToken.None);

            // assert

            result.Should().Be(Unit.Value);
            clientTrainingRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.Client.ClientTraining>()), Times.Never);

        }


        [Fact()]
        public async Task Handle_DoesntCreateCarWorkshopService_WhenUserIsNotAuthenticated()
        {
            // arrange

            var client = new Domain.Entities.Client.Client()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateClientTrainingCommand()
            {
                Date = new DateTime(2024, 9, 14),
                Description = "Service description",
                ClientEncodedName = "client1"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns((CurrentUser?)null);


            var clientRepositoryMock = new Mock<IClientRepository>();
            clientRepositoryMock.Setup(c => c.GetByEncodedName(command.ClientEncodedName))
                .ReturnsAsync(client);

            var clientTrainingRepositoryMock = new Mock<IClientTrainingRepository>();

            var handler = new CreateClientTrainingCommandHandler(userContextMock.Object, clientRepositoryMock.Object,
                clientTrainingRepositoryMock.Object);

            // act

            var result = await handler.Handle(command, CancellationToken.None);

            // assert

            result.Should().Be(Unit.Value);
            clientTrainingRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.Client.ClientTraining>()), Times.Never);

        }
    }
}