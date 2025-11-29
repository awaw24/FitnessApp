using Xunit;
using CoachBuddy.Application.ClientTraining.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace CoachBuddy.Application.ClientTraining.Commands.Tests
{
    public class CreateClientTrainingCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            // arrange

            var validator = new CreateClientTrainingCommandValidator();
            var command = new CreateClientTrainingCommand()
            {
                Description = "Description",
                Date = new DateTime(2024, 9, 14),
                ClientEncodedName= "Name"
            };

            // act

            var result = validator.TestValidate(command);

            //assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validate_WithInvalidCommand_ShouldHaveValidationErrors()
        {
            // arrange

            var validator = new CreateClientTrainingCommandValidator();
            var command = new CreateClientTrainingCommand()
            {
                Description = "",
                ClientEncodedName = null
            };

            // act

            var result = validator.TestValidate(command);

            //assert

            result.ShouldHaveValidationErrorFor(c => c.Description);
            result.ShouldHaveValidationErrorFor(c => c.ClientEncodedName);
        }
    }
}