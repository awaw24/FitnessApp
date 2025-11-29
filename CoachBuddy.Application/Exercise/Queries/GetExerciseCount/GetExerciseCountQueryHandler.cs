using CoachBuddy.Domain.Interfaces.Exercise;
using MediatR;

namespace CoachBuddy.Application.Exercise.Queries.GetExerciseCount
{
    public class GetExerciseCountQueryHandler : IRequestHandler<GetExerciseCountQuery, int>
    {
        private readonly IExerciseRepository _exerciseRepository;
        public GetExerciseCountQueryHandler(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }
        public async Task<int> Handle(GetExerciseCountQuery request, CancellationToken cancellationToken)
        {
            return await _exerciseRepository.GetExerciseCountAsync();
        }
    }
}
