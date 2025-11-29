using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Queries.GetTrainingPlanById
{
    public class GetTrainingPlanByIdQuery : IRequest<TrainingPlanDto>
    {
        public int Id { get; set; }
        public GetTrainingPlanByIdQuery(int id)
        {
            Id = id;
        }
    }
}
