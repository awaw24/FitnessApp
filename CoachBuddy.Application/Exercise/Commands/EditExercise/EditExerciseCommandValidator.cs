using FluentValidation;

namespace CoachBuddy.Application.Exercise.Commands.EditExercise
{
    public class EditExerciseCommandValidator:AbstractValidator<EditExerciseCommand>
    {
        public EditExerciseCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Exercise name is required.")
                .MaximumLength(100).WithMessage("Exercise name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.MuscleGroup)
                .NotEmpty().WithMessage("Muscle group is required.")
                .MaximumLength(50).WithMessage("Muscle group cannot exceed 50 characters.");
        }
    }
}
