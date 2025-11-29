using CoachBuddy.Domain.Interfaces.TrainingPlan;
using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Queries.GetTrainingPlanCount
{
    public class GetTrainingPlanCountQueryHandler : IRequestHandler<GetTrainingPlanCountQuery, int>
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        public GetTrainingPlanCountQueryHandler(ITrainingPlanRepository trainingPlanRepository)
        {
            _trainingPlanRepository = trainingPlanRepository;
        }
        public async Task<int> Handle(GetTrainingPlanCountQuery request, CancellationToken cancellationToken)
        {
            return await _trainingPlanRepository.GetTrainingPlanCountAsync();
        }
    }
}
