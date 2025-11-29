using Xunit;
using FluentAssertions;

namespace CoachBuddy.Domain.Entities.Tests
{
    public class ClientTests
    {
        [Fact()]
        public void EncodeName_ShouldSetEncodedName()
        {
            // arrange
            var client = new Client.Client();
            client.Name = "Test";
            client.LastName = "Client";

            // act

            client.EncodeName();

            // assert

            client.EncodedName.Should().Be("test-client");
        }

        [Fact()]
        public void EncodeName_ShouldThrowException_WhenNameIsNull()
        {
            // arrange
            var client = new Client.Client();

            // act

            Action action = () => client.EncodeName();

            // assert

            action.Invoking(a => a.Invoke())
                .Should().Throw<NullReferenceException>();
        }
    }
}