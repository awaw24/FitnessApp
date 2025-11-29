using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Commands.DeleteTrainingPlan
{
    public class DeleteTrainingPlanCommand : TrainingPlanDto, IRequest
    {
        public int Id { get; set; }
    }
}
