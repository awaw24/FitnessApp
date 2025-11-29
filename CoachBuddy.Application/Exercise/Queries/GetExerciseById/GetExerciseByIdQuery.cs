using MediatR;

namespace CoachBuddy.Application.Exercise.Queries.GetExerciseById
{
    public class GetExerciseByIdQuery : IRequest<ExerciseDto>
    {
        public int Id { get; set; }
        public GetExerciseByIdQuery(int id)
        {
            Id = id;
        }
    }
}
