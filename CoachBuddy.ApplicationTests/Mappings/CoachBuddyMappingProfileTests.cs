using Xunit;
using AutoMapper;
using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Application.CoachBuddy;
using Moq;
using CoachBuddy.Application.Client;
using FluentAssertions;

namespace CoachBuddy.Application.Mappings.Tests
{
    public class CoachBuddyMappingProfileTests
    {
        [Fact()]
        public void MappingProfile_ShouldMapClientDtoToClient()
        {
            // arrange

            var userContextMock = new Mock<IUserContext>();
            userContextMock
                .Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@example.com", new[] { "Moderator" }));


            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new CoachBuddyMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var dto = new ClientDto
            {
                City = "City",
                PhoneNumber = "123456789",
                PostalCode = "12345",
                Street = "Street"
            };

            // act

            var result = mapper.Map<Domain.Entities.Client.Client>(dto);

            // assert

            result.Should().NotBeNull();
            result.ContactDetails.Should().NotBeNull();
            result.ContactDetails.City.Should().Be(dto.City);
            result.ContactDetails.PhoneNumber.Should().Be(dto.PhoneNumber);
            result.ContactDetails.PostalCode.Should().Be(dto.PostalCode);
            result.ContactDetails.Street.Should().Be(dto.Street);

        }

        [Fact()]
        public void MappingProfile_ShouldMapClientToClientDto()
        {
            // arrange

            var userContextMock = new Mock<IUserContext>();
            userContextMock
                .Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@example.com", new[] { "Moderator" }));


            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new CoachBuddyMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var client = new Domain.Entities.Client.Client
            {
                Id = 1,
                CreatedById = "1",
                ContactDetails = new Domain.Entities.Client.ClientContactDetails
                {
                    City = "City",
                    PhoneNumber = "123456789",
                    PostalCode = "12345",
                    Street = "Street"
                }
            };

            // act

            var result = mapper.Map<ClientDto>(client);

            // assert

            result.Should().NotBeNull();

            result.IsEditable.Should().BeTrue();
            result.Street.Should().Be(client.ContactDetails.Street);
            result.City.Should().Be(client.ContactDetails.City);
            result.PostalCode.Should().Be(client.ContactDetails.PostalCode);
            result.PhoneNumber.Should().Be(client.ContactDetails.PhoneNumber);
        }
    }
}
