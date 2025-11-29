using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Commands.AddExerciseToTrainingPlan
{
    public class AddExerciseToTrainingPlanCommand : IRequest
    {
        public int TrainingPlanId { get; set; }
        public int ExerciseId {  get; set; }
    }
}
