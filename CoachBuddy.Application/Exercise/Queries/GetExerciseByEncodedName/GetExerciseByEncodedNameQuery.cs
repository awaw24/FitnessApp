using MediatR;

namespace CoachBuddy.Application.Exercise.Queries.GetExerciseByEncodedName
{
    public class GetExerciseByEncodedNameQuery : IRequest<ExerciseDto>
    {
        public string EncodedName { get; set; }
        public GetExerciseByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
