using FluentValidation;

namespace CoachBuddy.Application.Group.Commands.DeleteGroup
{
    public class DeleteGroupCommandValidator:AbstractValidator<DeleteGroupCommand>
    {
        public DeleteGroupCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Client ID must be greater than zero.");
        }
    }
}
