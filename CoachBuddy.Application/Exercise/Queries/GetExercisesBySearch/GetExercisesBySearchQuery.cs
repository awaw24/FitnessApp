using CoachBuddy.Application.Common;
using MediatR;

namespace CoachBuddy.Application.Exercise.Queries.GetExercisesBySearch
{
    public class GetExercisesBySearchQuery : IRequest<PaginatedResult<ExerciseDto>>
    {
        public string SearchTerm { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public GetExercisesBySearchQuery(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            SearchTerm = searchTerm;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
