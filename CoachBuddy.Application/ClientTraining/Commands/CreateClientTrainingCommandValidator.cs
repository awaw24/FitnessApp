using FluentValidation;

namespace CoachBuddy.Application.ClientTraining.Commands
{
    public class CreateClientTrainingCommandValidator:AbstractValidator<CreateClientTrainingCommand>
    {
        public CreateClientTrainingCommandValidator()
        {
            RuleFor(s => s.Date).NotEmpty().NotNull();
            RuleFor(s => s.Description).NotEmpty().NotNull();
            RuleFor(s => s.ClientEncodedName).NotEmpty().NotNull();
        }
    }
}
