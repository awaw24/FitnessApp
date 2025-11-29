using CoachBuddy.Application.Common;
using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Queries.GetAllTrainingPlans
{
    public class GetAllTrainingPlansQuery:IRequest<PaginatedResult<TrainingPlanDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
