using FluentValidation;

namespace CoachBuddy.Application.Group.Commands.CreateGroup
{
    public class CreateGroupCommandValidator:AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator()
        {RuleFor(g => g.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name should have at least 3 characters.")
                .MaximumLength(30).WithMessage("Name should have a maximum of 30 characters.");

            RuleFor(g => g.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(200).WithMessage("Description should have a maximum of 200 characters.");

            RuleFor(g => g.CreatedAt)
                .GreaterThan(DateTime.Now).WithMessage("Start date must be in the future."); 
            
        }
    }
}
