using FluentValidation;

namespace CoachBuddy.Application.TrainingPlan.Commands.CreateTrainingPlan
{
    public class CreateTrainingPlanCommandValidator:AbstractValidator<CreateTrainingPlanCommand>
    {
        public CreateTrainingPlanCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Training plan name is required.")
                .MaximumLength(100).WithMessage("Training plan name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
