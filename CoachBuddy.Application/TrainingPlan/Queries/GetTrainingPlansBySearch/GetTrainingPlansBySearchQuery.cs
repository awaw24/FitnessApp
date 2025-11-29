using CoachBuddy.Application.Common;
using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Queries.GetTrainingPlansBySearch
{
    public class GetTrainingPlansBySearchQuery : IRequest<PaginatedResult<TrainingPlanDto>>
    {
        public string SearchTerm { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public GetTrainingPlansBySearchQuery(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            SearchTerm = searchTerm;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
