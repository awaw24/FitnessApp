using MediatR;

namespace CoachBuddy.Application.Exercise.Commands.DeleteExercise
{
    public class DeleteExerciseCommand: ExerciseDto, IRequest
    {
        public int Id { get; set; }
    }
}
