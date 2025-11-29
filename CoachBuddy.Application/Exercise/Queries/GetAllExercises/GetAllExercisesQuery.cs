using CoachBuddy.Application.Common;
using MediatR;

namespace CoachBuddy.Application.Exercise.Queries.GetAllExercises
{
    public class GetAllExercisesQuery : IRequest<PaginatedResult<ExerciseDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
